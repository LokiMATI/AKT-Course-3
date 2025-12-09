using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models;

[Display(Name = "Сеанс")]
public partial class Session
{
    [Display(Name = "Идентификатор")]
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    public int SessionId { get; set; }

    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [Display(Name = "Фильм")]
    public int FilmId { get; set; }

    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [Display(Name = "Зал")]
    public byte HallId { get; set; }

    [Display(Name = "Цена")]
    [DataType(DataType.Currency)]
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [Range(0.01, 10000.00, ErrorMessage = "Цена должна быть от {1} до {2}")]
    public decimal Price { get; set; }

    [Display(Name = "Время сеанса")]
    [DataType(DataType.DateTime)]
    public DateTime SessionTime { get; set; }

    [Display(Name = "Это в 3D")]
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    public bool Is3d { get; set; }

    [Display(Name = "Фильм")]
    public virtual Film? Film { get; set; } = null!;

    [Display(Name = "Зал")]
    public virtual Hall? Hall { get; set; } = null!;

    [Display(Name = "Билеты")]
    public virtual ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
}
