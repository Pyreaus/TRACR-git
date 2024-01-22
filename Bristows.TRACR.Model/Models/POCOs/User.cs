using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.Models.POCOs;
public sealed partial class User
{    
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? FullName { get; set; } = string.Empty;
    public string Role { get; set; } = "Unauthorizeed";
    [Required]
    [MinLength(8), MaxLength(100)]
    [RegularExpression(@"^[a-fA-F0-9]+$", ErrorMessage = "Invalid format")]
    public byte[] StoredHash { get; init; } = Array.Empty<byte>();
    [Required]
    [MinLength(8), MaxLength(50)]
    public byte[] StoredSalt { get; init; } = Array.Empty<byte>();
    [Required]
    [MinLength(8), MaxLength(50)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]+$", ErrorMessage = "Invalid format")]
    public string Password { get; init; } = string.Empty;
    [Required]
    [EmailAddress(ErrorMessage = "Invalid format")]
    public string Email { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public object? Value { get; set; }
}