using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class LessonPreviousLesson
    {
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public int PreviousLessonId { get; set; }
        public Lesson PreviousLesson { get; set; }
    }
}