using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class SectionLesson
    {
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}