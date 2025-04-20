using API.Repositories.Interfaces;
using API.Data;
using API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class MembersRepository : IMembersRepository
    {
        private readonly ApplicationDbContext dbContext;
        public MembersRepository(ApplicationDbContext dbContext){
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Member>> GetAllAsync(){
            return await dbContext.Members.ToListAsync();
        }

       
    }
}