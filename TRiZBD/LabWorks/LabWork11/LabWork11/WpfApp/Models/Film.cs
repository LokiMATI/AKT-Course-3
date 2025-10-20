namespace WpfApp.Models
{
    public partial class Film
    {
        public int FilmId { get; set; }

        public string Title { get; set; } = null!;

        public short Duration { get; set; }

        public short Year { get; set; }

        public string? Description { get; set; }

        public byte[]? Poster { get; set; }

        public string? AgeRating { get; set; }

        public DateOnly? RentalStart { get; set; }

        public DateOnly? RentalEnd { get; set; }

        public bool IsDeleted { get; set; }
    }
}
