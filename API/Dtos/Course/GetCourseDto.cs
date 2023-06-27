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
        public string Duration { get; set; }
        public string PriceFull { get; set; }
        public string PriceMonthly { get; set; }
        public List<GetLessonDto> Lessons { get; set; }
    }
}