namespace API.Models.DTO
{
    public class ImageUploadRequest
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
    }
}

