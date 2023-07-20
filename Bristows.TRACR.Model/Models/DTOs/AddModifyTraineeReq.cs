using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.DTOs
{
    public partial class AddModifyTraineeReq
    {
        [Required]
        [MaxLength(50)]
        [Models.ValidationAttributes.ValidPfid]
        public string? REVIEWER_PFID { get; set; }
        public string? ACTIVE { get; set; }
        public string? SHOW { get; set; }
    }
}
