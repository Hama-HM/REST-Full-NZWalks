using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.IRepositories;

namespace NZWalks.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> _users = new List<User>()
        {
            new User()
            {
                FirstName ="Read Only", LastName ="User", Email ="readonly@gmail.com",
                Id = Guid.NewGuid(),Username="readonly@gmail.com", Password ="readonly@user",
            },
            new User()
            {
                FirstName ="Read Write", LastName ="User", Email ="readwrite@gmail.com",
                Id = Guid.NewGuid(),Username="readwrite@gmail.com", Password ="readwrite@user",
            }
        };
        public async Task<User> Authenticate(string username, string password)
        {
            var user = _users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) 
                    && x.Password ==password);

            return user !=null ? user : new();
        }
    }
}
