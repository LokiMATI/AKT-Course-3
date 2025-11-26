namespace WindowApp.Models;

public class TicketInfo
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime SeesionTime { get; set; }

    public string Cinema { get; set; }

    public int HallId { get; set; }

    public byte Row { get; set; }

    public byte Seat { get; set; }
}
