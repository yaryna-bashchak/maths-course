using API.Dtos.Lesson;

namespace API.Dtos.Section
{
    public class AddSectionDto
    {
        public int CourseId { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}