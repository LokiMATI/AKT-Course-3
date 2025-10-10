namespace CinemaDbLibrary.Helpers;

public record class Sorter
{
    public string ColumnName { get; set; } = null!;
    public bool IsAsc { get; set; } = true;
}
