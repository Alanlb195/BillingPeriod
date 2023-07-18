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
        [Display(Name ="Tiempo de la sesion en minutos")]
        public int DurationTime { get; set; } // MINUTES
    }
}
