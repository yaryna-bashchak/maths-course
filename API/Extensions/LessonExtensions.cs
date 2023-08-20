using API.Dtos.Course;

namespace API.Extensions
{
    public static class LessonExtensions
    {
        public static GetCourseDto Search(this GetCourseDto course, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return course;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            foreach (var section in course.Sections)
            {
                section.Lessons = section.Lessons.Where(l =>
                    l.Title.ToLower().Contains(lowerCaseSearchTerm) ||
                    l.Keywords.Any(k => k.Word.ToLower().Contains(lowerCaseSearchTerm))).ToList();
            }

            return course;
        }

        public static GetCourseDto Filter(this GetCourseDto course, int? maxImportance = 2, bool? onlyUncompleted = false)
        {
            if (maxImportance < 2)
            {
                foreach (var section in course.Sections)
                {
                    section.Lessons = section.Lessons.Where(l => l.Importance <= maxImportance).ToList();
                }
            }

            if (onlyUncompleted ?? false)
            {
                foreach (var section in course.Sections)
                {
                    section.Lessons = section.Lessons.Where(l => !l.IsPracticeCompleted || !l.IsTheoryCompleted || l.TestScore < 0).ToList();
                }
            }

            return course;
        }

        public static GetCourseDto SortAndRenumberLessons(this GetCourseDto course)
        {
            course.Sections = course.Sections.OrderBy(s => s.Number).ToList();

            for (int i = 0; i < course.Sections.Count; i++)
            {
                course.Sections[i].Lessons = course.Sections[i].Lessons.OrderBy(l => l.Number).ToList();
            }

            int lessonCounter = 1;
            foreach (var section in course.Sections)
            {
                foreach (var lesson in section.Lessons)
                {
                    lesson.Number = lessonCounter++;
                }
            }

            return course;
        }

        public static GetCoursePreviewDto SortAndRenumberLessons(this GetCoursePreviewDto course)
        {
            course.Sections = course.Sections.OrderBy(s => s.Number).ToList();

            for (int i = 0; i < course.Sections.Count; i++)
            {
                course.Sections[i].Lessons = course.Sections[i].Lessons.OrderBy(l => l.Number).ToList();
            }

            int lessonCounter = 1;
            foreach (var section in course.Sections)
            {
                foreach (var lesson in section.Lessons)
                {
                    lesson.Number = lessonCounter++;
                }
            }

            return course;
        }
    }
}