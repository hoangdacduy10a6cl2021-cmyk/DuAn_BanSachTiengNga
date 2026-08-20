using System.ComponentModel.DataAnnotations;

namespace QuanLySach.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите ваше имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите e-mail")]
        [EmailAddress(ErrorMessage = "Некорректный формат e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(6, ErrorMessage = "Минимум 6 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}