using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimal_api.Domain.DTO;
using minimal_api.Domain.Interfaces;
using minimal_api.Domain.Services;
using minimal_api.Infra.Db;
using Microsoft.OpenApi.Models;
using minimal_api.Domain.Entities;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("mysql"),
    new MySqlServerVersion(new Version(8, 0, 43)))
);
var app = builder.Build();
#endregion

#region Administrators
app.MapPost("/administrators/login", ([FromBody] LoginDTO loginDTO, IAdministratorService administratorService) =>
{
    if (administratorService.Login(loginDTO) != null)
        return Results.Ok("Login realizado com sucesso!");
    else
        return Results.Text("Login inválido, tente novamente.");
}).WithTags("Administrator");
#endregion

#region Vehicles
app.MapPost("/vehicles", (VehicleDTO vehicleDTO, IVehicleService vehicleService) =>
{
    if (string.IsNullOrEmpty(vehicleDTO.Name))
        return Results.BadRequest(new { message = "Name is required." });

    if (string.IsNullOrEmpty(vehicleDTO.Brand))
        return Results.BadRequest(new { message = "Brand is required." });

    if (vehicleDTO.Year < 1900 || vehicleDTO.Year > DateTime.Now.Year)
        return Results.BadRequest(new { message = $"Year must be between 1900 and {DateTime.Now.Year}."});

    var vehicle = new Vehicle
    {
        Name = vehicleDTO.Name,
        Brand = vehicleDTO.Brand,
        Year = vehicleDTO.Year
    };

    vehicleService.AddVehicle(vehicle);
    return Results.Created($"/vehicle/{vehicle.Id}", vehicle);
}).WithTags("Vehicle"); 

app.MapGet("/vehicles", (int? page, string? name, string? brand, IVehicleService vehicleService) =>
{
    var vehicles = vehicleService.ListAllVehicles(page ?? 1, name, brand);
    return Results.Ok(vehicles);
}).WithTags("Vehicle");;

app.MapGet("/vehicles/{id}", (int id, IVehicleService vehicleService) =>
{
    var vehicle = vehicleService.SearchById(id);
    if (vehicle == null)
        return Results.NotFound(new { message = "No vehicle was found with this ID."});
    else
        return Results.Ok(vehicle);
    
}).WithTags("Vehicle");

app.MapPut("/vehicles/{id}", (int id, VehicleDTO vehicleDTO, IVehicleService vehicleService) =>
{
    var vehicle = vehicleService.SearchById(id);
    if (vehicle == null)
        return Results.NotFound(new { message = "No vehicle was found with this ID." });

    if (string.IsNullOrEmpty(vehicleDTO.Name))
        return Results.BadRequest(new { message = "Name is required." });
    
    if (string.IsNullOrEmpty(vehicleDTO.Brand))
        return Results.BadRequest(new { message = "Brand is required." });

    if (vehicleDTO.Year < 1900 || vehicleDTO.Year > DateTime.Now.Year)
        return Results.BadRequest(new { message = $"Year must be between 1900 and {DateTime.Now.Year}."});

    vehicle.Name = vehicleDTO.Name;
    vehicle.Brand = vehicleDTO.Brand;
    vehicle.Year = vehicleDTO.Year;
    vehicleService.UpdateVehicle(vehicle);

    return Results.Accepted($"/vehicles/{vehicle.Id}", vehicle);
}).WithTags("Vehicle");

app.MapDelete("/vehicles/{id}", (int id, IVehicleService vehicleService) =>
{
    var vehicle = vehicleService.SearchById(id);
    if (vehicle == null)
        return Results.NotFound(new { message = "No vehicle was found with this ID." });
    vehicleService.DeleteVehicle(vehicle);

    return Results.Ok();
}).WithTags("Vehicle");
#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
#endregion