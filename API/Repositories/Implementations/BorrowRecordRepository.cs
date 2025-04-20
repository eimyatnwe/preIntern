using API.Repositories.Interfaces;
using API.Data;
using API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories.Implementations
{
    public class BorrowRecordRepository : IBorrowRecordRepository
    {
        private readonly ApplicationDbContext dbContext;
        public BorrowRecordRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BorrowRecord> CreateAsync(BorrowRecord borrowRecord)
        {
            await dbContext.BorrowRecord_Backup.AddAsync(borrowRecord);
            await dbContext.SaveChangesAsync();
            return borrowRecord;
        }

        public async Task<IEnumerable<BorrowRecord>> GetAllAsync()
        {
            return await dbContext.BorrowRecord_Backup.ToListAsync();
        }

        

       
    }
}