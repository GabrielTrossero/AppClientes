using AppClientes.Model.Services;
using AppClientes.ApplicationServices.Services;
using AppClientes.Model.Interfaces;
using AppClientes.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using AppClientes.Model.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using AppClientes.WebAPI.Models;
using AppClientes.WebAPI.Models.Profiles;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ClienteProfile));

// Configuracion de Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Relación entre las interfaces y sus implementaciones
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

// Configuración de Mapper
var config = new MapperConfiguration(cfg => cfg.CreateMap<ClienteUpdate, Cliente>());
var mapper = new Mapper(config);

// Permitir solicitudes del frontend
builder.Services.AddCors(options => options.AddPolicy(name: "ClientesOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));

// Conexión a la BD
builder.Services.AddDbContext<AppClientesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppClientesConnection")); // Toma los parametros desde el archivo appsettings.json
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ClientesOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
