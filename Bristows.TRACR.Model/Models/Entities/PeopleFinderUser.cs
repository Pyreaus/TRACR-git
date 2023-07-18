using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bristows.TRACR.Model.Models.ValidationAttributes;

namespace Bristows.TRACR.Model.Models.Entities
{
    [Table("PeopleFinderUser", Schema="dbo")]
    // [Keyless]
    public partial class PeopleFinderUser
    {
        [Key]
        [ValidPfid]
        public int PfId { get; set; } = 0;
        public Guid? LocalId { get; set; } = Guid.Empty;
        [Required]
        [MaxLength(10),MinLength(2)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(35),MinLength(2)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(40)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string? Email { get; set; }
        [MaxLength(40),MinLength(5)]
        [RegularExpression(@"^(\d{3}[- .]?){2}\d{4}$")]
        public string? Telephone { get; set; }
        [ValidPfid]
        public int? OtherPfid { get; set; } = 0;
        public string? Photo { get; set; } = string.Empty;
        [ValidWinUser]
        public string? WinUser { get; set; } = string.Empty;
    }
}