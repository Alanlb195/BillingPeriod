using BillingPeriod.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace BillingPeriod.Models
{
    public class UserData
    {
        [Required]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }


        [Required]
        [Display(Name = "Contraseña")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
            ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:")]
        public string Password { get; set; }

        [Required]
        [MinSeconds(10)]
        [Display(Name = "Tiempo de la sesión en segundos")]
        public int DurationTime { get; set; }

        [Required]
        [Display(Name = "Escribe lo que dice la imágen del Captcha")]
        public string Captcha { get; set; }

        public bool UseLettersInCaptcha { get; set; } // Preferencia para el tipo de CAPTCHA
    }
}
