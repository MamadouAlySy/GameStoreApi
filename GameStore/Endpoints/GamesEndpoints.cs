using GameStore.Dtos;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{
    private static readonly List<GameDto> _listOfGamesDtos = [
        new (1, "Street Fighting II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
        new (2, "FIFA 23", "Sports", 69.99M, new DateOnly(2022, 9, 27)),
    ]; 

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        const string getGameByIdEndPointName = "getGameByIdEnpoint";
        
        var group = app.MapGroup("games")
            .WithParameterValidation();

        group.MapGet("", () => _listOfGamesDtos);

        group.MapGet("{id}", (int id) => {
            GameDto? foundGameDto = _listOfGamesDtos.Find((game) => game.Id.Equals(id));
            return foundGameDto is null ? Results.NotFound() : Results.Ok(foundGameDto);
        }).WithName(getGameByIdEndPointName);

        group.MapPost("", (CreateGameDto newGame) => {
            GameDto game = new (
                _listOfGamesDtos.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            _listOfGamesDtos.Add(game);
            return Results.CreatedAtRoute(getGameByIdEndPointName, new {id = game.Id }, game);
        });

        group.MapPut("{id}", (int id, UpdateGameDto updatedGameData) => {
            var index = _listOfGamesDtos.FindIndex((game) => game.Id.Equals(id));
            if (index.Equals(-1)) 
                return Results.NotFound();
            _listOfGamesDtos[index] = new GameDto(
                id,
                updatedGameData.Name,
                updatedGameData.Genre,
                updatedGameData.Price,
                updatedGameData.ReleaseDate
            );
            return Results.NoContent();
        });

        group.MapDelete("{id}", (int id) => {
            var index = _listOfGamesDtos.FindIndex((game) => game.Id.Equals(id));
            _listOfGamesDtos.RemoveAt(index);
            return Results.NoContent();
        });

        return group;
    }
}
