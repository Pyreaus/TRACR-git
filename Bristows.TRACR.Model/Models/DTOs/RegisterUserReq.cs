using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.Models.POCOs;
public sealed partial class RegisterUserReq
{    
    public string? Id { get; internal init; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid format")]
    public string Email { get; internal init; } = string.Empty;
    [Required]
    [MinLength(8), MaxLength(50)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]+$", ErrorMessage = "Invalid format")]
    public string Password { get; internal init; } = string.Empty;
}