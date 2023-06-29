using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Section
{
    public class UpdateSectionDto
    {
        public int Number { get; set; } = -1;
        public string Title { get; set; }
        public string Description { get; set; }
        public int IsAvailable { get; set; } = -1;
        public List<int> LessonIdsToAdd { get; set; }
        public List<int> LessonIdsToDelete { get; set; }
    }
}