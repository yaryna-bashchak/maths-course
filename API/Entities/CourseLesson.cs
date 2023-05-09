using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class CourseLesson
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}