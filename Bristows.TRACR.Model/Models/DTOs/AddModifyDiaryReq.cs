using System.ComponentModel.DataAnnotations;
using Bristows.TRACR.Model.Models.ValidationAttributes;

namespace Bristows.TRACR.Model.DTOs
{
    public partial class AddModifyDiaryReq
    {
        [Required]
        [ValidPfid]
        public int Pfid { get; set; }
        public string? WeekBeginning { get; set; }
        public string? LearningPoints { get; set; }
        // public string? PracticeArea { get; set; }
        public string? ProfessionalDevelopmentUndertaken { get; set; }
        public string? ProfessionalConductIssues { get; set; }
        public bool? SignOffSubmitted { get; set; } = false;
        [MaxLength(10)]
        public string? SignedOffBy { get; set; }
        public bool? Show { get; set; } = true;
    }
}