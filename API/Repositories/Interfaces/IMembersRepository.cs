using API.Models.Domain;

namespace API.Repositories.Interfaces{
    public interface IMembersRepository
    {
        Task<IEnumerable<Member>> GetAllAsync();
    }
}