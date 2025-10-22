using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lection1017.Models;

public partial class Student
{
    public int Id { get; set; }

    [MinLength(3)]
    [MaxLength(100)]
    public string FullName { get; set; }

    [DataType(DataType.Date)]
    public DateTime Birthday { get; set; }

    public string Comment { get; set; }

    [Range(2.0, 5)]
    [Column(TypeName = "decimal(3,2)")]
    public decimal AverageMark { get; set; } = 5;

    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? Phone { get; set; }

    public string? Password { get; set; }

    [NotMapped]
    [Compare("Password")]
    public string? Confirm { get; set; }

    public virtual Group Group { get; set; } = null!;
}
