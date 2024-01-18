namespace API.Dtos.Lesson
{
    public class UpdateUserLesssonDto
    {
        public float TestScore { get; set; } = -1;
        public int IsTheoryCompleted { get; set; } = -1;
        public int IsPracticeCompleted { get; set; } = -1;
        public int courseId { get; set; } = -1;
    }
}