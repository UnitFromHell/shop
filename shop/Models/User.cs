using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace shop.Models;

public partial class User
{
    public int IdUser { get; set; }
    [Required(ErrorMessage = "Введите фамилию")]
    public string FirstNameUser { get; set; } = null!;
    [Required(ErrorMessage = "Введите имя")]
    public string SecondNameUser { get; set; } = null!;

    public string? MiddleNameUser { get; set; }
    [Required(ErrorMessage = "Введите логин")]
    public string LoginUser { get; set; } = null!;
    [Required(ErrorMessage = "Введите пароль")]
    public string PasswordUser { get; set; } = null!;

    [Required(ErrorMessage = "Введите номер телефона")]
    [RegularExpression(@"^\+7\(\d{3}\)-\d{3}-\d{2}-\d{2}$", ErrorMessage = "Введите действительный номер телефона")]
    public string PhoneNumber { get; set; } = null!;
    [Required(ErrorMessage = "Введите email")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Введите действительный адрес электронной почты")]
    public string Email { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
