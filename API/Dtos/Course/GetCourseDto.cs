using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Lesson;

namespace API.Dtos.Course
{
    public class GetCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<GetLessonForCourseDto> Lessons { get; set; }
    }
}