using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BillingPeriod.Models
{
    public class ActivityViewModel : IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Fecha y hora inicial")]
        [DataType(DataType.DateTime, ErrorMessage = "Fecha Inválida")]
        [CustomTimeRange("09:00", "16:00", ErrorMessage = "La actividad debe iniciar entre las 9:00 a.m. y las 4:00 p.m.")]
        public DateTime InitialDate { get; set; }

        [Required]
        [Display(Name = "Fecha de termino de la actividad")]
        [DataType(DataType.DateTime, ErrorMessage = "Fecha Inválida")]
        [CustomTimeRange("10:00", "17:00", ErrorMessage = "La actividad debe terminar entre las 10:00 a.m. y 05:00 p.m.")]
        public DateTime FinalDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            TimeSpan duration = FinalDate - InitialDate;

            if (duration.TotalMinutes < 30)
            {
                yield return new ValidationResult(
                    "La duración de la actividad debe ser por lo menos de 30 minutos",
                    new[] { nameof(FinalDate) });
            }

            if (FinalDate.Date != InitialDate.Date)
            {
                yield return new ValidationResult(
                    "No puedes programar una actividad dos fechas de diferentes días.",
                    new[] { nameof(FinalDate) });
            }
        }


    }


    public class CustomTimeRangeAttribute : ValidationAttribute
    {
        private readonly string _startTime;
        private readonly string _endTime;

        public CustomTimeRangeAttribute(string startTime, string endTime)
        {
            _startTime = startTime;
            _endTime = endTime;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                var startTime = DateTime.ParseExact(_startTime, "HH:mm", CultureInfo.InvariantCulture);
                var endTime = DateTime.ParseExact(_endTime, "HH:mm", CultureInfo.InvariantCulture);

                if (dateTime.TimeOfDay >= startTime.TimeOfDay && dateTime.TimeOfDay <= endTime.TimeOfDay)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }



}
