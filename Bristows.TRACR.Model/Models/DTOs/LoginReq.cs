using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.Models.POCOs;
public sealed partial class LoginReq
{    
    public string? Id { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid format")]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MinLength(8), MaxLength(50)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]+$", ErrorMessage = "Invalid format")]
    public string Password { get; set; } = string.Empty;
}