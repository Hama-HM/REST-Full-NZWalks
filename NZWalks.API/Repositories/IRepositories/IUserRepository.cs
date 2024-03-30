using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string username, string password);
    }
}
