using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bristows.TRACR.Model.Models.ValidationAttributes;

namespace Bristows.TRACR.Model.Models.Entities
{
    [Table("Admin", Schema="dbo")]
    public partial class Admin
    {
        [Key]
        public int AdminId { get; set; } = 0;
        public Guid? LocalId { get; set; } = Guid.Empty;
        [Required]
        [ValidPfid]
        public int ReviewerPfid { get; set; } = 0;
        public DateTime? EntryCreated { get; set; }
        public DateTime? LastUpdated { get; set; }
        [ValidPfid]
        public int? OtherPfid { get; set; } = 0;
        public bool? Active { get; set; } = false;
        public bool? Show { get; set; } = true;
    }
}