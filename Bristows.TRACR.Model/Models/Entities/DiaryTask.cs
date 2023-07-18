using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bristows.TRACR.Model.Models.Entities
{
    [Table("DiaryTask", Schema="dbo")]
    public partial class DiaryTask
    {
        [Key]
        public int DiaryTaskId { get; set; } = 0;
        [Required]
        public int DiaryId { get; set; } = 0;
        public Guid? LocalId { get; set; } = null;
        public ICollection<Skill>? Skills { get; set; } = new List<Skill>();
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string? Matter { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? TaskDescription { get; set; } = string.Empty;
        public DateTime? Timestamp { get; set; }
        public bool? Show { get; set; } = true;
    }
}