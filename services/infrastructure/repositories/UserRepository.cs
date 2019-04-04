using System.Threading.Tasks;
using Microservices.Services.Core.Entities;
using Microservices.Services.Infrastructure.Repositories;

namespace Microservices.Services.Infrastructure.Repositories
{
    public class UserRepository //: IUserRepository
    {
        public Task<User> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}