using System.ComponentModel.DataAnnotations;

namespace WorkshopClientApp.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Введите логин")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; }
}