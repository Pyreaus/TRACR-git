using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bristows.TRACR.Model.Models.ValidationAttributes;

namespace Bristows.TRACR.Model.Models.Entities
{
    [Table("Trainee", Schema="dbo")]
    public partial class Trainee
    {
        [Key]
        [Required]
        public int TraineeId { get; set; } = 0;
        public Guid? LocalId { get; set; } = Guid.NewGuid();
        [Required]
        [ValidPfid]
        public int TraineePfid { get; set; } = 0;
        [ValidPfid]
        public int ReviewerPfid { get; set; } = 0;
        public DateTime? EntryCreated { get; set; }
        public DateTime? LastUpdated { get; set; }
        [ValidPfid]
        public int? OtherPfid { get; set; } = 0;
        public bool? Active { get; set; } = true;
        public bool? Show { get; set; } = true;
    }
}