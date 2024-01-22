using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.DTOs
{
    public partial class AddModifyEmpReq
    {
        [Required]
        [MaxLength(30),MinLength(2)]
        public string? Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid format")]
        public string? Email { get; set; }
        [Required]
        [MaxLength(15),MinLength(10)]
        [RegularExpression("^[- +()0-9]{10,15}$", ErrorMessage = "Invalid format")]
        public string? Phone { get; set; }
        public bool? Show { get; set; }
    }
}
