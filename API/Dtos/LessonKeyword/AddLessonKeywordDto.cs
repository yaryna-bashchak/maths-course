using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.LessonKeyword
{
    public class AddLessonKeywordDto
    {
        public int LessonId { get; set; }
        public int KeywordId { get; set; }
    }
}