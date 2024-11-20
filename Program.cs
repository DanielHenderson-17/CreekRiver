using CreekRiver.Models;
using CreekRiver.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setup Npgsql and enable legacy timestamp behavior
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddNpgsql<CreekRiverDbContext>(builder.Configuration["CreekRiverDbConnectionString"]);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoint to get all campsites
app.MapGet("/api/campsites", (CreekRiverDbContext db) =>
{
    return db.Campsites
        .Select(c => new CampsiteDTO
        {
            Id = c.Id,
            Nickname = c.Nickname,
            ImageUrl = c.ImageUrl,
            CampsiteTypeId = c.CampsiteTypeId
        }).ToList();
});

// Endpoint to get a single campsite by Id, including related CampsiteType
app.MapGet("/api/campsites/{id}", async (CreekRiverDbContext db, int id) =>
{
    var campsite = await db.Campsites
        .Include(c => c.CampsiteType)
        .Select(c => new CampsiteDTO
        {
            Id = c.Id,
            Nickname = c.Nickname,
            CampsiteTypeId = c.CampsiteTypeId,
            CampsiteType = new CampsiteTypeDTO
            {
                Id = c.CampsiteType.Id,
                CampsiteTypeName = c.CampsiteType.CampsiteTypeName,
                FeePerNight = c.CampsiteType.FeePerNight,
                MaxReservationDays = c.CampsiteType.MaxReservationDays
            }
        })
        .FirstOrDefaultAsync(c => c.Id == id);

    if (campsite == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(campsite);
});

app.Run();
