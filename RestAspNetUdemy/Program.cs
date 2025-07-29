using EvolveDb;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
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
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(
	options => options.UseMySql(connection,
		new MySqlServerVersion(new Version(9, 0, 5)))
);

var mysqlConnectionTemplate = builder.Configuration["MySQLConnection:MySQLConnectionString"];
if (string.IsNullOrEmpty(mysqlConnectionTemplate))
	throw new Exception("Configuração MySQLConnection:MySQLConnectionString não encontrada no appsettings.json!");

var mysqlPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
if (string.IsNullOrEmpty(mysqlPassword))
	throw new Exception("Variável de ambiente MYSQL_PASSWORD não definida!");

var mysqlConnectionString = mysqlConnectionTemplate.Replace("__MYSQL_PASSWORD__", mysqlPassword)
												  + ";AllowPublicKeyRetrieval=True;SslMode=None;";
builder.Services.AddDbContext<MySQLContext>(options =>
	options.UseMySql(mysqlConnectionString,
		new MySqlServerVersion(new Version(9, 4, 0))));

if (builder.Environment.IsDevelopment())
{
	MigrateDatabase(connection);
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

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

// Versioning API
builder.Services.AddApiVersioning();

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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