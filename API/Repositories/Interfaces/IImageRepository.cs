using API.Models.Domain;
namespace API.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<BookImage?> Upload(IFormFile file, BookImage bookImage);
        bool ValidateFileType(string fileExtension);
        bool ValidateFileSize(long fileSize);
        Task<IEnumerable<BookImage>> GetAll();
    }
}