using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Keyword;
using SQLitePCL;

namespace API.Dtos.Lesson
{
    public class GetLessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlTheory { get; set; }
        public string UrlPractice { get; set; }
        public int Number { get; set; }
        public int Importance { get; set; } = 0;
        public int TestScore { get; set; } = -1;
        public bool IsTheoryCompleted { get; set; } = false;
        public bool IsPracticeCompleted { get; set; } = false;
        public List<GetKeywordDto> Keywords { get; set; }
        public List<GetPreviousLessonDto> PreviousLessons { get; set; }
    }
}