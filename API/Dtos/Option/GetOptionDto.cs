namespace API.Dtos.Option
{
    public class GetOptionDto
    {
        public int TestId { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public string ImgUrl { get; set; }
        public string PublicId { get; set; }
        public bool isAnswer { get; set; }
    }
}