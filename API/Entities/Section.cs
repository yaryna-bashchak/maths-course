using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public int Number { get; set; } = 1;
        public string Title { get; set; } = "Місяць 1";
        public string Description { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public List<SectionLesson> SectionLessons { get; set; }
        public List<UserSection> UserSections { get; set; }
    }
}