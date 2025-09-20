using LearnWell.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnWell.Infrastructure.Data
{
    public class LearnWellDbContext : DbContext
    {
        public LearnWellDbContext(DbContextOptions<LearnWellDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        public DbSet<CourseClass> CourseClasses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CourseClass>().HasKey(cc => new { cc.CourseId, cc.ClassId });
            modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
            modelBuilder.Entity<StudentClass>().HasKey(sc => new { sc.StudentId, sc.ClassId });

            modelBuilder.Entity<StudentClass>()
                .HasOne(sc => sc.AssignedBy)
                .WithMany()
                .HasForeignKey(sc => sc.AssignedByStaffId);
        }
    }
}
