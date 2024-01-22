
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bristows.TRACR.Model.Models.Entities
{
    [Keyless]
    [Table("PFUser", Schema="dbo")]
    public sealed partial class PeopleFinderUser
    {
        [MaxLength(3)]
        public int? PFID { get; set; } = 0;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string WinUser { get; set; } = string.Empty;
        public int? DepartmentID { get; set; } = 0;
        public string? Initials { get; set; }
        // private string? _initials;
        // public string Initials
        // {
        //     get => _initials ?? $"{FirstName?.FirstOrDefault()}{LastName?.FirstOrDefault()}";
        //     set => _initials = value;
        // }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? FeeEarnerChargeOutRate { get; set; } = 0;
        public string? JobTitle { get; set; }
        [EmailAddress(ErrorMessage = "Invalid format")]
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public string? Mobile { get; set; } 
        public string? Photo { get; set; }
        public int? SecretaryID { get; set; } = 0;
        public int? LocationID { get; set; } = 0;
        public int? RoomID { get; set; } = 0;
        public DateTime? JoinDate { get; set; }
        public DateTime? MaternityDate { get; set; }
        public bool? MaternityDirection { get; set; } = false;
        public DateTime? SecondmentDate { get; set; }
        public string? SecondmentCompanyName { get; set; }
        public string? About { get; set; }
        public DateTime? LeaveDate { get; set; }
        public bool ActiveUser { get; set; } = true;
        public bool Partner { get; set; } = false;
        public bool Human { get; set; } = false;
        public DateTime? Timestamped { get; set; }
        public int? CreatorID { get; set; }
        public bool? FeeEarner { get; set; } = false;
        public bool? Show { get; set; } = false;
        public string? Upn { get; set; }
        public DateTime? SabbaticalBeginDate { get; set; }
        public DateTime? SabbaticalEndDate { get; set; }
        public string? PronouncedAs { get; set; }
    }    
}