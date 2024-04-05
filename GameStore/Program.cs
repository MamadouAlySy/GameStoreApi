using GameStore.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string getGameByIdEndPointName = "getGameByIdEnpoint";

List<GameDto> listOfGamesDtos = [
    new (1, "Street Fighting II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
    new (2, "FIFA 23", "Sports", 69.99M, new DateOnly(2022, 9, 27)),
]; 

app.MapGet("/", () => "");

app.MapGet("/games", () => listOfGamesDtos);

app.MapGet("/games/{id}", (int id) => 
    listOfGamesDtos.Find((game) => game.Id.Equals(id))
).WithName(getGameByIdEndPointName);

app.MapPost("/games", (CreateGameDto newGame) => {
    GameDto game = new (
        listOfGamesDtos.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    listOfGamesDtos.Add(game);
    return Results.CreatedAtRoute(getGameByIdEndPointName, new {id = game.Id }, game);
});

app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGameData) => {
    var index = listOfGamesDtos.FindIndex((game) => game.Id.Equals(id));
    listOfGamesDtos[index] = new GameDto(
        id,
        updatedGameData.Name,
        updatedGameData.Genre,
        updatedGameData.Price,
        updatedGameData.ReleaseDate
    );
    return Results.NoContent();
});

app.MapDelete("/games/{id}", (int id) => {
    var index = listOfGamesDtos.FindIndex((game) => game.Id.Equals(id));
    listOfGamesDtos.RemoveAt(index);
    return Results.NoContent();
});

app.Run();
