using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bristows.TRACR.Model.Models.ValidationAttributes;

namespace Bristows.TRACR.Model.Models.Entities
{
    [Table("Diary", Schema="dbo")]
    public partial class Diary
    {
        [Key]
        public int DiaryId { get; set; } = 0;
        public Guid? LocalId { get; set; } = null;
        [Required]
        [ValidPfid]
        public int Pfid { get; set; } = 0;
        public string? WeekBeginning { get; set; }
        // public string? PracticeArea { get; set; } = string.Empty;
        public string? LearningPoints { get; set; } = string.Empty;
        public string? ProfessionalDevelopmentUndertaken { get; set; } = string.Empty;
        public string? ProfessionalConductIssues { get; set; } = string.Empty;
        public bool? SignOffSubmitted { get; set; } = false;
        [MaxLength(10)]
        public string? SignedOffBy { get; set; } = string.Empty;
        public DateTime? SignedOffTimestamp { get; set; }
        public DateTime? Timestamp { get; set; }
        public bool? Show { get; set; } = true;
    }
}
