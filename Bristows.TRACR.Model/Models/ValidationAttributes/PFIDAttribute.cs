using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.Models.ValidationAttributes
{
    public class ValidPfid : ValidationAttribute
    {
        private const int MinValue = 2, MaxValue = 5;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int)
            {
                string pfidString = ((int)value).ToString();
                return ValidatePfidString(pfidString, validationContext);
            }
            else if (value is string pfidString) return ValidatePfidString(pfidString, validationContext);
            return new ValidationResult($"The field {validationContext.DisplayName} must be an integer or a string representing an integer between {MinValue} and {MaxValue}.");
        }
        private ValidationResult ValidatePfidString(string pfidString, ValidationContext validationContext)
        {
            if (pfidString.Length >= MinValue && pfidString.Length <= MaxValue && int.TryParse(pfidString, out int pfid))
            {
                return ValidationResult.Success!;
            }
            return new ValidationResult($"The field {validationContext.DisplayName} must be an integer or a string representing an integer between {MinValue} and {MaxValue}.");
        }
    }
}
