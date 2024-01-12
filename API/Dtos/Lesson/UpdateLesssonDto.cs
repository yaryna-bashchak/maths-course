using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Lesson
{
    public class UpdateLesssonDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile TheoryFile { get; set; }
        public IFormFile PracticeFile { get; set; }
        public int Number { get; set; } = -1;
        public int Importance { get; set; } = -1;
        public float TestScore { get; set; } = -1;
        public int IsTheoryCompleted { get; set; } = -1;
        public int IsPracticeCompleted { get; set; } = -1;
        public int courseId { get; set; } = -1;
    }
}