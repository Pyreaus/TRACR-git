using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.DTOs
{
    public partial class AddModifyDiaryReq
    {
        [Required]
        [Models.ValidationAttributes.ValidPfid]
        public string? PFID { get; set; }
        public string? PRACTICE_AREA { get; set; }
        public DateTime? WEEK_BEGINNING { get; set; }
        public string? LEARNING_POINTS { get; set; }
        public string? PROFESSIONAL_DEVELOPMENT_UNDERTAKEN { get; set; }
        public string? PROFESSIONAL_CONDUCT_ISSUES { get; set; }
        [MaxLength(50)]
        public string? SIGN_OFF_SUBMITTED { get; set; }
        [MaxLength(255)]
        public string? SIGNED_OFF_BY { get; set; }
        [MaxLength(50)]
        public string? SHOW { get; set; }
    }
}