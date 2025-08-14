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
using minimal_api.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using minimal_api.Api.Domain.Enums;

#region Builder
var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(key)) throw new Exception("JWT Key is not configured.");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();    
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("mysql"),
    new MySqlServerVersion(new Version(8, 0, 43)))
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter the JWT token like this: Bearer {your token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
var app = builder.Build();
#endregion

#region Administrators

string GetTokenJwt(Administrator administrator, string key)
{
    if (string.IsNullOrEmpty(key)) return string.Empty;

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim>()
    {
        new Claim("Email", administrator.Email),
        new Claim("Profile", administrator.Profile.ToString()),
        new Claim(ClaimTypes.Role, administrator.Profile.ToString())
    };
    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.Now.AddDays(1),
        signingCredentials: credentials
    );
    return new JwtSecurityTokenHandler().WriteToken(token);
}

app.MapGet("/administrators", ([FromQuery] int? page, IAdministratorService administratorService) =>
{
    var admins = administratorService.ListAllAdministrators(page)
     .Select(a => new AdministratorOutDTO
     {
         Id = a.Id,
         Email = a.Email,
         Profile = a.Profile
     })
     .ToList();

    return Results.Ok(admins);
})  
.RequireAuthorization(new AuthorizeAttribute {Roles = "Administrator" })
.WithTags("Administrators");

app.MapGet("/administrators/{id}", (int? id, IAdministratorService administratorService) =>
{
    var administrator = administratorService.SearchAdministratorById(id);

    if (administrator == null)
        return Results.NotFound(new { message = "Administrator with this ID was not found." });

    var returnDto = new AdministratorOutDTO
    {
        Id = administrator.Id,
        Email = administrator.Email,
        Profile = administrator.Profile
    };

    return Results.Ok(returnDto);    
})  
.RequireAuthorization(new AuthorizeAttribute {Roles = "Administrator" })
.WithTags("Administrators");


app.MapPost("/administrators/login", ([FromBody] LoginDTO loginDTO, IAdministratorService administratorService) =>
{
    var adm = administratorService.Login(loginDTO);
    if (adm != null)
    {
        string token = GetTokenJwt(adm, key);

        var result = new
        {
            Email = adm.Email,
            Profile = adm.Profile,
            Token = token
        };

        return Results.Ok(result);
    }
    else
    {
        return Results.Unauthorized();
    }
}).WithTags("Authentication");

app.MapPost("/administrators", ([FromBody] AdministratorDTO administratorDTO, IAdministratorService administratorService) =>
{
    if (string.IsNullOrEmpty(administratorDTO.Email))
        return Results.BadRequest(new { message = "Email is required." });

    if (string.IsNullOrEmpty(administratorDTO.Password))
        return Results.BadRequest(new { message = "Password is required." });

    if (!Enum.IsDefined(typeof(Profile), administratorDTO.Profile))
    return Results.BadRequest(new { message = "Profile is required." });

    var administrator = new Administrator
    {
        Email = administratorDTO.Email,
        Password = administratorDTO.Password,
        Profile = administratorDTO.Profile
    };

    var createdAdmin = administratorService.Create(administrator);

     var adminDto = new AdministratorOutDTO
    {
        Id = createdAdmin.Id,
        Email = createdAdmin.Email,
        Profile = createdAdmin.Profile
    };

    return Results.Created($"/administrators/{createdAdmin.Id}", adminDto);
}).RequireAuthorization(new AuthorizeAttribute {Roles = "Administrator" }).WithTags("Administrators");
#endregion

#region Vehicles

app.MapGet("/vehicles", (int? page, string? name, string? brand, IVehicleService vehicleService) =>
{
    var vehicles = vehicleService.ListAllVehicles(page ?? 1, name, brand);
    return Results.Ok(vehicles);
})
.RequireAuthorization(new AuthorizeAttribute { Roles = "Administrator,Editor" }).WithTags("Vehicle");


app.MapGet("/vehicles/{id}", (int id, IVehicleService vehicleService) =>
{
    var vehicle = vehicleService.SearchById(id);
    if (vehicle == null)
        return Results.NotFound(new { message = "No vehicle was found with this ID." });
    else
        return Results.Ok(vehicle);

})
.RequireAuthorization(new AuthorizeAttribute { Roles = "Administrator,Editor" }).WithTags("Vehicle");

app.MapGet("/vehicles/by-contract-type", (ContractType contractType, IVehicleService vehicleService,bool onlyActive ,int? page) =>
{
    var vehicles = vehicleService.ListAllVehiclesPerContractType(contractType, onlyActive, page ?? 1);
    if (vehicles == null)
        return Results.NotFound(new { message = "No vehicles found for the specified contract type." });

    return Results.Ok(vehicles);
})
.RequireAuthorization(new AuthorizeAttribute { Roles = "Administrator,Editor" }).WithTags("Vehicle");

app.MapGet("/vehicles/by-plate/{plate}", (string plate, IVehicleService vehicleService) =>
{
    var vehicle = vehicleService.SearchByPlate(plate);
    if (vehicle == null)
        return Results.NotFound(new { message = "No vehicles was found with this ID." });
    else
        return Results.Ok(vehicle);
})
.RequireAuthorization(new AuthorizeAttribute { Roles = "Administrator,Editor" }).WithTags("Vehicle");
#endregion

#region App
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
#endregion