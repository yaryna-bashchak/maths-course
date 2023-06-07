using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CourseContext : DbContext
    {
        public CourseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LessonKeyword>()
                .HasKey(lk => new { lk.LessonId, lk.KeywordId });  
            modelBuilder.Entity<LessonKeyword>()
                .HasOne(lk => lk.Lesson)
                .WithMany(l => l.LessonKeywords)
                .HasForeignKey(lk => lk.LessonId);  
            modelBuilder.Entity<LessonKeyword>()
                .HasOne(lk => lk.Keyword)
                .WithMany(k => k.LessonKeywords)
                .HasForeignKey(lk => lk.KeywordId);

            modelBuilder.Entity<CourseLesson>()
                .HasKey(cl => new { cl.CourseId, cl.LessonId });  
            modelBuilder.Entity<CourseLesson>()
                .HasOne(cl => cl.Course)
                .WithMany(c => c.CourseLessons)
                .HasForeignKey(cl => cl.CourseId);
            modelBuilder.Entity<CourseLesson>()
                .HasOne(cl => cl.Lesson)
                .WithMany(l => l.CourseLessons)
                .HasForeignKey(cl => cl.LessonId);

            modelBuilder.Entity<Test>()
                .HasMany(t => t.Options)
                .WithOne(o => o.Test)
                .HasForeignKey(o => o.TestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.Tests)
                .WithOne(t => t.Lesson)
                .HasForeignKey(t => t.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LessonPreviousLesson>()
                .HasKey(lpl => new { lpl.LessonId, lpl.PreviousLessonId });
            modelBuilder.Entity<LessonPreviousLesson>()
                .HasOne(lpl => lpl.Lesson)
                .WithMany(l => l.PreviousLessons) // <--
                .HasForeignKey(lpl => lpl.LessonId);
            modelBuilder.Entity<LessonPreviousLesson>()
                .HasOne(lpl => lpl.PreviousLesson)
                .WithMany(pl => pl.PreviousLessonOf) // <--
                .HasForeignKey(lpl => lpl.PreviousLessonId);
        }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseLesson> CourseLessons { get; set; }
        public DbSet<LessonPreviousLesson> PreviousLessons { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<LessonKeyword> LessonKeywords { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Option> Options { get; set; }
    }
}