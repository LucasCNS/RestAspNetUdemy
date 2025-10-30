using EvolveDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using RestAspNetUdemy.Business;
using RestAspNetUdemy.Business.Implementations;
using RestAspNetUdemy.Hypermedia.Enricher;
using RestAspNetUdemy.Hypermedia.Filters;
using RestAspNetUdemy.Model.Context;
using RestAspNetUdemy.Repository;
using RestAspNetUdemy.Repository.Generic;
using RestAspNetUdemy.Services.Implementation;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementations;
using RestWithASPNETUdemy.Configurations;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Services;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var appName = "REST API's RESTful from 0 to Azure with ASP.NET Core 8 and Docker";
var appVersion = "v1";
var appDescription = $"REST API RESTful developed in course '{appName}'";

// Add services to the container.

var tokenConfigurations = new TokenConfiguration();

new ConfigureFromConfigurationOptions<TokenConfiguration>(
		builder.Configuration.GetSection("TokenConfigurations")
	)
	.Configure(tokenConfigurations);

builder.Services.AddSingleton(tokenConfigurations);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = tokenConfigurations.Issuer,
		ValidAudience = tokenConfigurations.Audience,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
	};
});

builder.Services.AddAuthorization(auth =>
{
	auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
		.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
		.RequireAuthenticatedUser().Build());
});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
	builder.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader();
}));

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc(appVersion,
		new OpenApiInfo
		{
			Title = appName,
			Version = appVersion,
			Description = appDescription,
			Contact = new OpenApiContact
			{
				Name = "Leandro Costa",
				Url = new Uri("https://pub.erudio.com.br/meus-cursos")
			}
		});
});

var mysqlConnectionTemplate = builder.Configuration["MySQLConnection:MySQLConnectionString"];
if (string.IsNullOrEmpty(mysqlConnectionTemplate))
	throw new Exception("Configuração MySQLConnection:MySQLConnectionString não encontrada no appsettings.json!");

var mysqlPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
if (string.IsNullOrEmpty(mysqlPassword))
	throw new Exception("Variável de ambiente MYSQL_PASSWORD não definida!");

var mysqlConnectionString = mysqlConnectionTemplate.Replace("__MYSQL_PASSWORD__", mysqlPassword)
												  + ";AllowPublicKeyRetrieval=True;SslMode=Preferred;";
builder.Services.AddDbContext<MySQLContext>(options =>
	options.UseMySql(mysqlConnectionString,
		new MySqlServerVersion(new Version(9, 4, 0))));

if (builder.Environment.IsDevelopment())
{
	MigrateDatabase(mysqlConnectionString);
}

builder.Services.AddMvc(options =>
{
	options.RespectBrowserAcceptHeader = true;

	options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
	options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
}).AddXmlSerializerFormatters();

var filterOptions = new HyperMediaFilterOptions();

filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

builder.Services.AddSingleton(filterOptions);
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped<IFileBusiness, FileBusinessImplementation>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddTransient<ILoginBusiness, LoginBusinessImplementation>();
builder.Services.AddTransient<ITokenService, TokenService>();


// Versioning API
builder.Services.AddApiVersioning();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json",
	$"{appName} - {appVersion}");
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

app.Run();

void MigrateDatabase(string connection)
{
	try
	{
		var evolveConnection = new MySqlConnection(connection);
		var evolve = new Evolve(evolveConnection, Log.Information)
		{
			Locations = new List<string> { "db/migrations", "db/dataset" },
			IsEraseDisabled = true,
		};
		evolve.Migrate();
	}
	catch (Exception ex)
	{
		Log.Error("Database migration failed", ex);
		throw;
	}
}