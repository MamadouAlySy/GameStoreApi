using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos;

public record class CreateGameDto(
    [Required]
    [StringLength(50)]
    string Name,

    [Required]
    [StringLength(50)]
    string Genre,

    [Required]
    [Range(0, 1000)]
    decimal Price,

    [Required]
    DateOnly ReleaseDate
);

