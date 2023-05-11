using API.Entities;

namespace API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CourseContext context)
        {
            if (context.Lessons.Any()) return;

            var lessons = new List<Lesson>{};

            foreach (var lesson in lessons)
            {
                context.Lessons.Add(lesson);
            }

            context.SaveChanges();
        }
    }
}