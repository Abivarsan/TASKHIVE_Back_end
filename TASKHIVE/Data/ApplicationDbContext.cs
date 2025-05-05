using Microsoft.EntityFrameworkCore;
using TASKHIVE.Model;

namespace TASKHIVE.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<TimeLog> TimeLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkSpace> WorkSpaces { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Store UserRole Enum as String
            modelBuilder.Entity<Role>()
                .Property(r => r.userRole)
                .HasConversion<string>();

            // Role → Users (One-to-Many)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.roleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Users → TimeLogs (One-to-Many)
            modelBuilder.Entity<TimeLog>()
                .HasOne(tl => tl.User)
                .WithMany(u => u.TimeLogs)
                .HasForeignKey(tl => tl.userId)
                .OnDelete(DeleteBehavior.Cascade);

            // Work → TimeLogs (One-to-Many) 🔥 **Fix this line**
            modelBuilder.Entity<TimeLog>()
                .HasOne(tl => tl.Work)
                .WithMany(w => w.TimeLogs)
                .HasForeignKey(tl => tl.workId)
                .OnDelete(DeleteBehavior.Cascade); 

            // Users → Reports (One-to-Many)
            modelBuilder.Entity<Report>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.userId)
                .OnDelete(DeleteBehavior.Cascade);

            // Project -> Work (One-to-Many) 🔥 **Fix this line**
            modelBuilder.Entity<Work>()
                .HasOne(w => w.Project)
                .WithMany(p => p.Works)
                .HasForeignKey(w => w.projectId)
                .OnDelete(DeleteBehavior.Cascade); 

            // WorkSpace → Project  (One-to-Many)
            modelBuilder.Entity<Project>()
                .HasOne(w => w.WorkSpace)
                .WithMany(p => p.Projects)
                .HasForeignKey(w => w.workSpaceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
