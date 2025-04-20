using API.Models.Domain;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public interface IBorrowRecordRepository
    {
        Task<BorrowRecord> CreateAsync(BorrowRecord borrowRecord);
        Task<IEnumerable<BorrowRecord>> GetAllAsync();
    }
}