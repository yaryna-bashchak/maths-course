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
        public bool isCompleted { get; set; } = false;
        public List<Keyword> Keywords { get; set; }
        public List<Lesson> PreviousLessons { get; set; }
        public List<Test> Tests { get; set; }
    }
}