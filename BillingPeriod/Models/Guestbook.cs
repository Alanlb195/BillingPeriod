using System.ComponentModel.DataAnnotations;

namespace BillingPeriod.Models
{
    public class Guestbook
    {
        public int Id { get; set; }

        [Display(Name ="Nombre")]
        public string Name { get; set; }

        [Display(Name = "País")]
        public string Country { get; set; }

        [Display(Name = "Comentario")]
        public string Comment { get; set; }
    }
}
