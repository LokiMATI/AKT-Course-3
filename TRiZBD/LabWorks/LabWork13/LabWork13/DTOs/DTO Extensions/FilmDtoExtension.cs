using LabLibrary.Models;

namespace LabWork13.DTOs.DTO_Extensions;

public static class FilmDtoExtension
{
    public static FilmDto ToDto(this Film film)
        => new()
        {
            Id = film.FilmId,
            Title = film.Title
        };
}
