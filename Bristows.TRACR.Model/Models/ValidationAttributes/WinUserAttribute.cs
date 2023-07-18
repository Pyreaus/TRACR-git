using System.ComponentModel.DataAnnotations;

public class ValidWinUser : RegularExpressionAttribute
{
    public ValidWinUser() : base(@"^[A-Za-z0-9_\-]+\\[A-Za-z0-9_\-]+$")
    {
        ErrorMessage = "The field {0} must be in the format 'DOMAIN\\User' and have a length between 4 and 20 characters.";
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var minLengthAttribute = new MinLengthAttribute(4);
        var maxLengthAttribute = new MaxLengthAttribute(20);

        if (!minLengthAttribute.IsValid(value))
            return new ValidationResult($"The field {validationContext.DisplayName} must have a minimum length of {minLengthAttribute.Length} characters.");
        if (!maxLengthAttribute.IsValid(value))
            return new ValidationResult($"The field {validationContext.DisplayName} must have a maximum length of {maxLengthAttribute.Length} characters.");

        return base.IsValid(value, validationContext);
    }
}
