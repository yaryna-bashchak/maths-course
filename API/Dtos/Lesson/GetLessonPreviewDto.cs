namespace API.Dtos.Lesson
{
    public class GetLessonPreviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public int Importance { get; set; } = 0;
    }
}