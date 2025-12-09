using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models;

[Display(Name = "Посетитель")]
public partial class Visitor
{
    [Display(Name = "Идентификатор")]
    public int VisitorId { get; set; }

    [Display(Name = "Телефон")]
    [DataType(DataType.PhoneNumber)]
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [Phone(ErrorMessage = "Некорректный номер телефона")]
    [MinLength(10, ErrorMessage = "Минимальная длина номера телефона 10")]
    public string Phone { get; set; } = null!;

    [Display(Name = "Имя")]
    [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ\s]+$", ErrorMessage = "Только буквы и пробелы")]
    public string? Name { get; set; }

    [Display(Name = "Дата рождения")]
    [DataType(DataType.Date)]
    public DateTime? Birthday { get; set; }

    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Некорректный email адрес")]
    public string? Email { get; set; }

    [Display(Name = "Билеты")]
    public virtual ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
}
