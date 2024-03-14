using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.Models.ViewModels;
public sealed partial class DevUserViewModel
{    
    public string? AuthToken { get; set; } 
    public DateTime? AuthTokenExpiration { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    [Required]
    public string Role { get; set; } = "Unauthorizeed";
    [Required]
    [EmailAddress(ErrorMessage = "Invalid format")]
    public string Email { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public object? Value { get; set; }
}