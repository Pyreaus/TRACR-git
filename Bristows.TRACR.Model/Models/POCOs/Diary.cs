using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bristows.TRACR.Model.Models.Entities
{
    [Table("DIARY", Schema="dbo")]
    public sealed partial class Diary
    {
        [Key]
        public int DIARY_ID { get; set; } = 0;
        [MaxLength(50)]
        [ValidationAttributes.ValidPfid]
        public string? PFID { get; set; } = string.Empty;
        [MaxLength(255)]
        public string? PRACTICE_AREA { get; set; } = string.Empty;
        public DateTime? WEEK_BEGINNING { get; set; }
        public string? LEARNING_POINTS { get; set; } = string.Empty;
        public string? PROFESSIONAL_DEVELOPMENT_UNDERTAKEN { get; set; } = string.Empty;
        public string? PROFESSIONAL_CONDUCT_ISSUES { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? SIGN_OFF_SUBMITTED { get; set; } = string.Empty;
        [MaxLength(255)]
        public string? SIGNED_OFF_BY { get; set; } = string.Empty;
        public DateTime? SIGNED_OFF_TIMESTAMP { get; set; }
        public DateTime? TIMESTAMP { get; set; }
        [MaxLength(50)]
        public string? SHOW { get; set; } = string.Empty;
    }
}
