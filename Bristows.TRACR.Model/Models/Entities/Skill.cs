using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bristows.TRACR.Model.Models.Entities
{
    [Table("SKILLS", Schema = "dbo")]
    public partial class Skill
    {
        [Key]
        public int? SKILL_ID { get; set; } = 0;
        [Required]
        [MaxLength(500),MinLength(3)]
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string? SKILL_NAME { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? SKILL_DESCRIPTION { get; set; } = string.Empty;
        public string? SHOW { get; set; } = "true";
        public string? COLOUR { get; set; } = string.Empty;
    }
}