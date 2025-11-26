namespace TimetableWindowApp.Dtos;

public class SessionDto
{
    public string Title { get; set; } = null!;

    public TimeSpan SessionTime { get; set; }

    public byte HallNumber { get; set; }

    public string Cinema { get; set; } = null!;

    public decimal Price { get; set; }
}
