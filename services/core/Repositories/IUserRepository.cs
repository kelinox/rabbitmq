using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Services.Core.Entities;

namespace Microservices.Services.Core.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> AddNewUser(User user);
    }
}