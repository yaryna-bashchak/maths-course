using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<Option> Options { get; set; }
        public string Type { get; set; }
        public string ImgUrl { get; set; } = "";
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}