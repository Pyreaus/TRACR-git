using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.DTOs
{
    public partial class AddModifyDiaryReq
    {
        [Required]
        [Models.ValidationAttributes.ValidPfid]
        public string? PFID { get; set; }
        public string? WEEK_BEGINNING { get; set; }
        public string? LEARNING_POINTS { get; set; }
        // public string? PracticeArea { get; set; }
        public string? ProfessionalDevelopmentUndertaken { get; set; }
        public string? ProfessionalConductIssues { get; set; }
        public bool? SignOffSubmitted { get; set; } = false;
        [MaxLength(10)]
        public string? SignedOffBy { get; set; }
        public bool? Show { get; set; } = true;
    }
}