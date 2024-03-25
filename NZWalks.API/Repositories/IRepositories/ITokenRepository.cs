using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories.IRepositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
