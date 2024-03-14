using GameOfLife.API.Extensions;
using GameOfLife.Application;
using GameOfLife.Model.Configuration;
using GameOfLife.Model.DTOs;
using GameOfLife.Model.Entities;
using GameOfLife.Model.Services;
using GameOfLife.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddPersistence();
builder.Services.AddAutoMapper(opt => opt.AddMaps(typeof(Board).Assembly));
builder.Services.Configure<GameSettings>(builder.Configuration.GetSection(nameof(GameSettings)));

builder.Logging.AddConsole().AddDebug();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapPost("/board", async ([FromServices] IBoardAppService boardApp, [FromBody] BoardRequestDto dto)
    => (await boardApp.CreateBoardAsync(dto)).AsResult());

app.MapGet("/board/{id}/next", async ([FromServices] IBoardAppService boardApp, [FromQuery] Guid id)
    => (await boardApp.GetNextBoardStateAsync(id)).AsResult());

app.MapGet("/board/{id}/{stateNumber}", async ([FromServices] IBoardAppService boardApp, [FromQuery] Guid id, [FromQuery] long stateNumber)
    => (await boardApp.GetBoardStateAsync(id, stateNumber)).AsResult());

app.MapGet("/board/{id}/final", async ([FromServices] IBoardAppService boardApp, [FromQuery] Guid id)
    => (await boardApp.GetBoardFinalStateAsync(id)).AsResult());

await app.RunAsync();
