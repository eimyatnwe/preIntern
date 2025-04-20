using Microsoft.AspNetCore.Identity;
namespace API.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}