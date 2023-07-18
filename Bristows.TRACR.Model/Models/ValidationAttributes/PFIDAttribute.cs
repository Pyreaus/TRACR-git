using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.Models.ValidationAttributes
{
    public class ValidPfid : ValidationAttribute
    {
        private const int MinValue = 2;
        private const int MaxValue = 5;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int pfid && pfid.ToString().Length >= MinValue && pfid.ToString().Length <= MaxValue)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"The field {validationContext.DisplayName} must be an integer between {MinValue} and {MaxValue}.");
        }
    }
}