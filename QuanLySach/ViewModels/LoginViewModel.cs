using System.ComponentModel.DataAnnotations;

namespace QuanLySach.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите e-mail")]
        [EmailAddress(ErrorMessage = "Некорректный формат e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}