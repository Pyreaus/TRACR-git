using Bristows.TRACR.Model.Models.Entities;
using Bristows.TRACR.Model.Models.Entities.Employees;
using Microsoft.EntityFrameworkCore;

namespace Bristows.TRACR.Model.Contexts
{
    public partial class TRACRContext : DbContext
    {
        public TRACRContext(DbContextOptions<TRACRContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {                                       // Fallback connection string in case options are not configured
                optionsBuilder.UseSqlServer("Server=SRV-SQL04;Database=TRACR_DEV; User ID=DB_USER;Password=intranet;MultipleActiveResultSets=True;Encrypt=False;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<PeopleFinderUser> PFUser { get; set;}
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Diary> Diaries { get; set; }
        public virtual DbSet<DiaryTask> DiaryTasks { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Trainee> Trainees { get; set; }
        public virtual DbSet<Employee> Employees { get; set; } //development only

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeopleFinderUser>().ToView("PeopleFinderUser").HasKey(x=>x.PFID);
        }
    }
}
