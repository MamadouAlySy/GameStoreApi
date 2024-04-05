using GameStore.Dtos;
using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new { message = "Welocome to the game API"});

app.MapGamesEndpoints();

app.Run();
