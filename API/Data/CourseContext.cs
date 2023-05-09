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
            modelBuilder.Entity<Test>()
                .HasOne(t => t.Lesson)
                .WithMany(l => l.Tests);

            modelBuilder.Entity<Option>()
                .HasOne(o => o.Test)
                .WithMany(t => t.Options);

        }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Option> Options { get; set; }
    }
}