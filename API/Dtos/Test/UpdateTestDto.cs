using API.Dtos.Option;

namespace API.Dtos.Test
{
    public class UpdateTestDto
    {
        public string Question { get; set; }
        public string Type { get; set; }
        public IFormFile File { get; set; }
        public List<int> OptionIdsToAdd { get; set; }
        public List<int> OptionIdsToDelete { get; set; }
    }
}