using System.ComponentModel.DataAnnotations;
using Bristows.TRACR.Model.Models.ValidationAttributes;

namespace Bristows.TRACR.Model.DTOs
{
    public partial class AddModifyTraineeReq
    {
        [Required]
        [ValidPfid]
        public int? ReviewerPfid { get; set; }
        public bool? Active { get; set; } = true;
        public bool? Show { get; set; } = true;
    }
}