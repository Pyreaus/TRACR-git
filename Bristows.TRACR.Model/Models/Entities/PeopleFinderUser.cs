
namespace Bristows.TRACR.Model.Models.Entities
{
    public partial class PeopleFinderUser
    {
        public int? PFID { get; set; } = 0;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? FullName { get; set; } = string.Empty;
        public string? WinUser { get; set; } = string.Empty;
        public int? DepartmentID { get; set; } = 0;
        public string? JobTitle { get; set; } = string.Empty;
        public string? Telephone { get; set; } = string.Empty;
        public string? Mobile { get; set; } = string.Empty;
        public string? Photo { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public int? SecretaryID { get; set; } = 0;
        public int? LocationID { get; set; } = 0;
        public int? RoomID { get; set; } = 0;
        public DateTime? JoinDate { get; set; }
        public DateTime? MaternityDate { get; set; }
        public bool? MaternityDirection { get; set; } = false;
        public DateTime? SecondmentDate { get; set; }
        public string? SecondmentCompanyName { get; set; } = string.Empty;
        public DateTime? LeaveDate { get; set; }
        public string? About { get; set; } = string.Empty;
        public bool? ActiveUser { get; set; } = true;
        public bool? Partner { get; set; } = false;
        public bool? Human { get; set; } = false;
        public DateTime Timestamped { get; set; }
        public int? CreatorID { get; set; } = 0;
        public string? Initials { get; set; } = string.Empty;
        public bool? FeeEarner { get; set; } = false;
        public bool? Show { get; set; } = false;
        public decimal? FeeEarnerChargeOutRate { get; set; } = 0;
        public string? Upn { get; set; } = string.Empty;
        public DateTime? SabbaticalBeginDate { get; set; }
        public DateTime? SabbaticalEndDate { get; set; }
        public string? PronouncedAs { get; set; } = string.Empty;
    }    
}