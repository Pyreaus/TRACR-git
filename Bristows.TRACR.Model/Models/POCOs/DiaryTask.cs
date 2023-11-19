using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bristows.TRACR.Model.Models.Entities
{
    [Table("DIARY_TASKS", Schema="dbo")]
    public partial class DiaryTask
    {
        [Key]
        public int DIARY_TASK_ID { get; set; } = 0;
        [Required]
        public int DIARY_ID { get; set; } = 0;
        [MaxLength(500)]
        public string? MATTER { get; set; } = string.Empty;
        public string? FEE_EARNERS { get; set; } = string.Empty;
        public string? TASK_DESCRIPTION { get; set; } = string.Empty;
        public string? SKILLS { get; set; } = string.Empty;
        public DateTime? TIMESTAMP { get; set; }
        [MaxLength(50)]
        public string? SHOW { get; set; } = string.Empty;
    }
}