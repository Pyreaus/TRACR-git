using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.Models.POCOs;
public sealed partial class RegisterUserReq
{   
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? FullName { get; set; } = string.Empty;
    [Required]
    [EmailAddress(ErrorMessage = "Invalid format")]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MinLength(8), MaxLength(50)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$",
    ErrorMessage = "Invalid Format: password must contain a number and a special character")]
    public string Password { get; set; } = string.Empty;
}