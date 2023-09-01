namespace API.Entities
{
    public class UserSection
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public bool isAvailable { get; set; } = false;
    }
}