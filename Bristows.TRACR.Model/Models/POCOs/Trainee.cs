using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bristows.TRACR.Model.Models.Entities
{
    [Table("TRAINEES", Schema="dbo")]
    public sealed partial class Trainee
    {
        [Key]
        public int TRAINEE_ID { get; set; } = 0;
        [ValidationAttributes.ValidPfid]
        [MaxLength(50)]
        public string? TRAINEE_PFID { get; set; } = string.Empty;
        [ValidationAttributes.ValidPfid]
        [MaxLength(50)]
        public string? REVIEWER_PFID { get; set; } = string.Empty;
        [ValidationAttributes.ValidPfid]
        [MaxLength(50)]
        public string? OTHER_PFID { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? ACTIVE { get; set; } = "true";
        [MaxLength(50)]
        public string? SHOW { get; set; } = "true";
    }
}