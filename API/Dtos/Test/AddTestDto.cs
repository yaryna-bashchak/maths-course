using API.Dtos.Option;

namespace API.Dtos.Test
{
    public class AddTestDto
    {
        public int LessonId { get; set; }
        public string Question { get; set; }
        public string Type { get; set; } = "radio";
        public IFormFile File { get; set; }
        // public List<int> OptionIds { get; set; }
    }
}