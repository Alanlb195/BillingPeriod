using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingPeriod.Models
{
    public class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdActivity { get; set; }
        public string Name { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public ICollection<Note> Notes { get; set; }
    }

    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNote { get; set; }
        public string Text { get; set; }

        [ForeignKey("IdActivity")]
        public Activity Activitie { get; set; }
    }
}
