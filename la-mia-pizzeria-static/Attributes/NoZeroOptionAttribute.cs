using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Attributes
{
    public class NoZeroOptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext _)
        {
            var input = value as int?;

            if (input is null or 0)
            {
                return new ValidationResult($"Inserire almeno un valore");
            }

            return ValidationResult.Success!;
        }
    }
}
