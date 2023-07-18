using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.DTOs
{
    public partial class AddModifyEmpReq
    {
        [Required]
        [MaxLength(30),MinLength(2)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(15),MinLength(10)]
        [RegularExpression("^[- +()0-9]{10,15}$")]
        public string? Phone { get; set; }
        [Required]
        [MaxLength(30),MinLength(6)]
        public string? Email { get; set; }
        public bool? Show { get; set; } = true;
    }
}
