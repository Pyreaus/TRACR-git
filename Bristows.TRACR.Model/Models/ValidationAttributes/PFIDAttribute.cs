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
                int pfid = (int)value;
                return ValidatePfid(pfid, validationContext);
            }
            else if (value is string pfidString)
            {
                if (int.TryParse(pfidString, out int pfid)) return ValidatePfid(pfid, validationContext);
            }
            return new ValidationResult($"The field {validationContext.DisplayName} must be an integer between {MinValue} and {MaxValue}.");
        }
        private static ValidationResult ValidatePfid(int pfid, ValidationContext validationContext)
        {
            if (pfid >= MinValue && pfid <= MaxValue) return ValidationResult.Success!;
            return new ValidationResult($"The field {validationContext.DisplayName} must be an integer between {MinValue} and {MaxValue}.");
        }
    }
}