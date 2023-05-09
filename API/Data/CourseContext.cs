using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CourseContext : DbContext
    {
        public CourseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Lesson> Lessons { get; set; }
    }
}