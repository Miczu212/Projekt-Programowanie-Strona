using System.ComponentModel.DataAnnotations;
namespace tysjyfgjkhfghjetsrstr.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Pole Nazwa użytkownika jest wymagane.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest wymagane.")]
        public string Password { get; set; }

    }
}
