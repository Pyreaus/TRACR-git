using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bristows.TRACR.Model.Models.Entities
{
    [Table("ADMINS", Schema="dbo")]
    public sealed partial class Admin
    {
        [Key]
        public int AID { get; set; } = 0;
        [MaxLength(50)]
        public string? WINUSER { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? FULL_NAME { get; set; } = string.Empty;
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Invalid format")]
        public string? EMAIL { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? ADMIN { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? ACTIVE_USER { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? ROLE { get; set; } = string.Empty;
    }
}