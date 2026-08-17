using Dapper;
using Microsoft.Data.Sqlite;
using AeroManage.Core.Interfaces;
using AeroManage.Core.Services;
using AeroManage.Infrastructure.Data;
using AeroManage.Infrastructure.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// --- Injection de dépendances ---
builder.Services.AddSingleton<IDbConnectionFactory>(
    new SqliteConnectionFactory(builder.Configuration.GetConnectionString("Default")!));
//Avion
builder.Services.AddScoped<IAvionRepository, AvionRepository>();
builder.Services.AddScoped<IAvionService, AvionService>();
//Aeroport
builder.Services.AddScoped<IAeroportRepository, AeroportRepository>();
builder.Services.AddScoped<IAeroportService, AeroportService>();
//Passager
builder.Services.AddScoped<IPassagerRepository, PassagerRepository>();
builder.Services.AddScoped<IPassagerService, PassagerService>();
//Personnel
builder.Services.AddScoped<IPersonnelRepository, PersonnelRepository>();
builder.Services.AddScoped<IPersonnelService, PersonnelService>();
//Reservation
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
//Vol
builder.Services.AddScoped<IVolRepository, VolRepository>();
builder.Services.AddScoped<IVolService, VolService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors("AllowAngular");

// --- Création de la base au démarrage ---
using (var connection = new SqliteConnection(builder.Configuration.GetConnectionString("Default")))
{
    connection.Open();
    var schemaSql = File.ReadAllText("../Database/schema.sql");
    connection.Execute(schemaSql);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();