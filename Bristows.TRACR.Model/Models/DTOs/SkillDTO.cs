using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.DTOs
{
    public partial class SkillDTO
    {
        [Required]
        [MaxLength(20),MinLength(3)]
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string SkillName { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? SkillDescription { get; set; } = string.Empty;
        public bool? Show { get; set; } = true;
        public string? Colour { get; set; } = string.Empty;
    }
}
