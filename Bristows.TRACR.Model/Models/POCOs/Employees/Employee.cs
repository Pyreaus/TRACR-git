using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bristows.TRACR.Model.Models.Entities.Employees
{
    [Table("Employee", Schema="dbo")]
    public partial class Employee
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(30),MinLength(2)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(15),MinLength(10)]
        [RegularExpression("^[- +()0-9]{10,15}$")]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [MaxLength(30),MinLength(6)]
        public string Email { get; set; } = string.Empty;
        public DateTime? EntryCreated { get; set; }
        public DateTime? LastUpdated { get; set; }
        [Required]
        public bool? Show { get; set; } = true;
    }
}
