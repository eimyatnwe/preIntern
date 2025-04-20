using API.Models.Domain;

namespace API.Repositories.Interfaces
{
    public interface IBooksRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetById(Guid id);
        Task<IEnumerable<Book>> GetByCategory(string category);
        Task<Book> CreateAsync(Book book);
        Task<Book?> DeleteAsync(Guid id);
        Task<Book?> UpdateAsync(Book book);
    }
}