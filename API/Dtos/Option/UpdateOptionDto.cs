namespace API.Dtos.Option
{
    public class UpdateOptionDto
    {
        public string Text { get; set; }
        public IFormFile File { get; set; }
        public int isAnswer { get; set; } = -1;
    }
}