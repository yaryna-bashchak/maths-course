using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Lesson
{
    public class GetPreviousLessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float? TestScore { get; set; }
        public bool IsTheoryCompleted { get; set; } = false;
        public bool IsPracticeCompleted { get; set; } = false;
    }
}