namespace API.Entities
{
    public class UserLesson
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public bool IsTheoryCompleted { get; set; } = false;
        public bool IsPracticeCompleted { get; set; } = false;
        public float? TestScore { get; set; }
    }
}