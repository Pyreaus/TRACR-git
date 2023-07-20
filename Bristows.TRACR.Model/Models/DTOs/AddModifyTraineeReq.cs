using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.DTOs
{
    public partial class AddModifyTraineeReq
    {
        [Required]
        [MaxLength(50)]
        [Models.ValidationAttributes.ValidPfid]
        public string? REVIEWER_PFID { get; set; } = string.Empty;
        public string? ACTIVE { get; set; } = "true";
        public string? SHOW { get; set; } = "true";
    }
}
