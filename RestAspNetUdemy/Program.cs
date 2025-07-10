using Microsoft.EntityFrameworkCore;
using RestAspNetUdemy.Business;
using RestAspNetUdemy.Business.Implementations;
using RestAspNetUdemy.Model.Context;
using RestAspNetUdemy.Repository;
using RestAspNetUdemy.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(
	options => options.UseMySql(connection,
		new MySqlServerVersion(new Version(9, 0, 5)))
);

// Versioning API
builder.Services.AddApiVersioning();

builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();

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

app.Run();
