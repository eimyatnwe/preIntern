using API.Repositories.Interfaces;
using API.Data;
using API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class BooksRepository : IBooksRepository
    {
        private readonly ApplicationDbContext dbContext;
        public BooksRepository(ApplicationDbContext dbContext){
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(){
            return await dbContext.Books.ToListAsync();
        }

        public async Task<Book?> GetById(Guid id){
            
            return await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Book>> GetByCategory(string category){
            return await dbContext.Books.Where(x => x.Category.ToLower() == category.ToLower()).ToListAsync();
        }

        public async Task<Book> CreateAsync(Book book){
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> DeleteAsync(Guid id){
            var existingBook = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if(existingBook is null){
                return null;
            }
            dbContext.Books.Remove(existingBook);
            await dbContext.SaveChangesAsync();
            return existingBook;

        }

        public async Task<Book?> UpdateAsync(Book book){
            var existingBook = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == book.Id);
            if(existingBook is null){
                return null;
            }
            dbContext.Entry(existingBook).CurrentValues.SetValues(book);
            await dbContext.SaveChangesAsync();
            return book;
        }
    }
}