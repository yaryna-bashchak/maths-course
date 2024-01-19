using API.Dtos.Option;

namespace API.Dtos.Test
{
    public class GetTestDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public string ImgUrl { get; set; }
        public string PublicId { get; set; }
        public List<GetOptionDto> Options { get; set; }
    }
}