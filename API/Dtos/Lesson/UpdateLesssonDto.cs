using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Lesson
{
    public class UpdateLesssonDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlTheory { get; set; }
        public string UrlPractice { get; set; }
        public int Number { get; set; } = -1;
        public int Importance { get; set; } = -1;
        public int TestScore { get; set; } = -1;
        public int IsTheoryCompleted { get; set; } = -1;
        public int IsPracticeCompleted { get; set; } = -1;
    }
}