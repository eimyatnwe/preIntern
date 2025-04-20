using API.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){

        }

        public DbSet<Book> Books {get;set;}
        public DbSet<Member> Members {get;set;}
        public DbSet<BorrowRecord> BorrowRecord_Backup {get;set;}
        public DbSet<BookImage> BookImages {get;set;}
    }
}