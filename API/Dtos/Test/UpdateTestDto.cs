using API.Dtos.Option;

namespace API.Dtos.Test
{
    public class UpdateTestDto
    {
        public int LessonId { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public IFormFile File { get; set; }
        // public List<UpdateOptionDto> Options { get; set; }
    }
}