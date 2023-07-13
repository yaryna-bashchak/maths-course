using API.Dtos.Lesson;

namespace API.Dtos.Section
{
    public class GetSectionPreviewDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; } = false;
        public List<GetLessonPreviewDto> Lessons { get; set; }
    }
}