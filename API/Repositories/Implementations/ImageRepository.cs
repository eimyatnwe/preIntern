using API.Repositories.Interfaces;
using API.Models.Domain;
using API.Data;
using Microsoft.EntityFrameworkCore; 

namespace API.Repositories.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext dbContext;
        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private const long MaxFileSize = 10 * 1024 * 1024; // 10MB

        public ImageRepository(
            IWebHostEnvironment webHostEnvironment, 
            IHttpContextAccessor httpContextAccessor, 
            ApplicationDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public bool ValidateFileType(string fileExtension)
        {
            return allowedExtensions.Contains(fileExtension.ToLower());
        }

        public bool ValidateFileSize(long fileSize)
        {
            return fileSize <= MaxFileSize;
        }
        
        public async Task<BookImage?> Upload(IFormFile file, BookImage bookImage)
        {
            try
            {
                // Ensure Images directory exists
                var uploadsFolder = Path.Combine(webHostEnvironment.ContentRootPath, "Images");
                Directory.CreateDirectory(uploadsFolder);

                // Create file path
                var localPath = Path.Combine(uploadsFolder, $"{bookImage.FileName}{bookImage.FileExtension}");

                // Save file
                using (var stream = new FileStream(localPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Generate URL
                var httpRequest = httpContextAccessor.HttpContext?.Request;
                if (httpRequest != null)
                {
                    bookImage.Url = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{bookImage.FileName}{bookImage.FileExtension}";
                }

                // Save to database
                await dbContext.BookImages.AddAsync(bookImage);
                await dbContext.SaveChangesAsync();
                return bookImage;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<BookImage>> GetAll(){
            return await dbContext.BookImages.ToListAsync();
        }
    }
}