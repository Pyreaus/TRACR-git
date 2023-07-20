using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bristows.TRACR.Model.Models.Entities
{
    [Table("People", Schema="dbo")]
    // [Keyless]
    public partial class PeopleFinderUser
    {
        [Key]
        [ValidationAttributes.ValidPfid]
        public int? PFID { get; set; } = 0;
        [MinLength(2),MaxLength(50)]
        [Required]
        public string? FirstName { get; set; } = string.Empty;
        [MinLength(2),MaxLength(50)]
        [Required]
        public string? LastName { get; set; } = string.Empty;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MinLength(2),MaxLength(101)]
        [Required]
        public string? FullName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? WinUser { get; set; } = string.Empty;
        [Required]
        public int? DepartmentID { get; set; } = 0;
        [Required]
        [MaxLength(100)]
        public string? JobTitle { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string? Telephone { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? Mobile { get; set; } = string.Empty;
        [MaxLength(260)]
        public string? Photo { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Email { get; set; } = string.Empty;
        public int? SecretaryID { get; set; } = 0;
        public int? LocationID { get; set; } = 0;
        public int? RoomID { get; set; } = 0;
        public DateTime? JoinDate { get; set; }
        public DateTime? MaternityDate { get; set; }
        public bool? MaternityDirection { get; set; } = false;
        public DateTime? SecondmentDate { get; set; }
        [MaxLength(50)]
        public string? SecondmentCompanyName { get; set; } = string.Empty;
        public DateTime? LeaveDate { get; set; }
        public string? About { get; set; } = string.Empty;
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool? ActiveUser { get; set; } = true;
        [Required]
        public bool? Partner { get; set; } = false;
        [Required]
        public bool? Human { get; set; } = false;
        [Required]
        public DateTime Timestamped { get; set; }
        [Required]
        public int? CreatorID { get; set; } = 0;
        [MaxLength(10)]
        public string? Initials { get; set; } = string.Empty;
        [Required]
        public bool? FeeEarner { get; set; } = false;
        [Required]
        public bool? Show { get; set; } = false;
        public decimal? FeeEarnerChargeOutRate { get; set; } = 0;
        [MaxLength(50)]
        public string? Upn { get; set; } = string.Empty;
        public DateTime? SabbaticalBeginDate { get; set; }
        public DateTime? SabbaticalEndDate { get; set; }
        [MaxLength(260)]
        public string? PronouncedAs { get; set; } = string.Empty;
    }    
}