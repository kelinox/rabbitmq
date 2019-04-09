using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Services.Core.Entities;

namespace Microservices.Services.Core.Interface.Services
{
    public interface IUserService
    {
        Task<User> AddNewUser(User user);
        Task<IEnumerable<User>> GetAll();
    }
}