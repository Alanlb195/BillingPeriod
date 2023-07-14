using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace BillingPeriod.Models;
public class UserCookieData
{
    private string nombre;

    [Display(Name = "Nombre, solo letras y no permite espacio doble")]
    [RegularExpression(@"^[a-zA-Z]+( [a-zA-Z]+)*$", ErrorMessage = "El nombre solo puede contener letras y no se permite el doble espacio.")]
    public string Nombre
    {
        get { return nombre; }
        set { nombre = EliminarDobleEspacio(value); }
    }

    // Resto del código...

    private static string EliminarDobleEspacio(string texto)
    {
        return Regex.Replace(texto, @"\s+", " ").Trim();
    }

    [Display(Name = "Correo, solo acepta correos válidos")]
    [EmailAddress(ErrorMessage ="Email invalido")]
    public string Correo { get; set; }

    [Display(Name ="Fecha y Hora de expiración")]
    public DateTime Expiracion { get; set; }
}
