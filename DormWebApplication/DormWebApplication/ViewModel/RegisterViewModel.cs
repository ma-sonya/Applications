using System.ComponentModel.DataAnnotations;

namespace DormWebApplication.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
                
        [Display (Name ="ПІБ")]
        public string Name { get; set; } = null!;

        [Display(Name ="Рік народження")]
        [Range(1940, 2022, ErrorMessage ="Введіть дійсну дату народження")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name ="Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Compare("Password", ErrorMessage ="Паролі не співпадають")]
        [Display(Name ="Підтвердження паролю")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
