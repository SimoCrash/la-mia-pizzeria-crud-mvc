using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Attributes
{
    public class ParoleMultipleAttribute : ValidationAttribute
    {
        int contaParole;
        public ParoleMultipleAttribute(int contaParole)
        {
            this.contaParole = contaParole;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext _)
        {
            var input = value as string;

            if (input is null || input.Trim().Split(' ').Length < contaParole)
            {
                return new ValidationResult($"Inserire almeno {contaParole} parole");
            }

            return ValidationResult.Success!;
        }
    }
}

