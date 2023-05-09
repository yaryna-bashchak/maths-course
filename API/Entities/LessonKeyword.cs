using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class LessonKeyword
    {
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }
    }
}