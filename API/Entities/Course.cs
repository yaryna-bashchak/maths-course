using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; } = 1;
        public int PriceFull { get; set; }
        public int PriceMonthly { get; set; }
        public List<CourseLesson> CourseLessons { get; set; }
    }
}