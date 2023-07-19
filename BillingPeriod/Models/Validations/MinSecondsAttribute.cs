using System.ComponentModel.DataAnnotations;

namespace BillingPeriod.Models.Validations
{
    public class MinSecondsAttribute : ValidationAttribute
    {
        private readonly int _minSeconds;

        public MinSecondsAttribute(int minSeconds)
        {
            _minSeconds = minSeconds;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int seconds && seconds >= _minSeconds)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"El valor debe ser mayor o igual a {_minSeconds} segundos.");
        }
    }
}