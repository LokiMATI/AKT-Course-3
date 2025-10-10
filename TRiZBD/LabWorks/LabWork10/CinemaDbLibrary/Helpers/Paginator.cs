namespace CinemaDbLibrary.Helpers;

public record class Paginator
{
    public int PageSize { get; set; } = 3;
    public int PageNumber { get; set; } = 1;
}
