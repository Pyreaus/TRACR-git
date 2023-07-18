using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bristows.TRACR.Model.Models.Entities
{
    [Table("Skill", Schema = "dbo")]
    public partial class Skill
    {
        [Key]
        public int SkillId { get; set; } = 0;
        public Guid? LocalId { get; set; } = null;
        [Required]
        [MaxLength(20),MinLength(3)]
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string? SkillName { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? SkillDescription { get; set; } = string.Empty;
        public bool? Show { get; set; } = true;
        public string? Colour { get; set; } = string.Empty;
    }
}