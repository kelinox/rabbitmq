using System.Threading.Tasks;
using Microservices.Services.Infrastructure.Entities;
using Microservices.Services.Infrastructure.Repositories;

namespace Microservices.Services.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}