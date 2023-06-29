namespace API.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlTheory { get; set; }
        public string UrlPractice { get; set; }
        public int Number { get; set; }
        public int Importance { get; set; } = 0;
        public int TestScore { get; set; } = -1;
        public bool IsTheoryCompleted { get; set; } = false;
        public bool IsPracticeCompleted { get; set; } = false;
        public List<SectionLesson> SectionLessons { get; set; }
        public List<LessonKeyword> LessonKeywords { get; set; }
        public List<LessonPreviousLesson> PreviousLessons { get; set; } = new List<LessonPreviousLesson>();
        public List<LessonPreviousLesson> PreviousLessonOf { get; set; } = new List<LessonPreviousLesson>();
        public List<Test> Tests { get; set; }
    }
}