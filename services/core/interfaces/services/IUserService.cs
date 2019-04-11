using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Models;

namespace Microservices.Services.Core.Interface.Services
{
    public interface IUserService
    {
        Task<User> AddNewUser(User user);
        Task<IEnumerable<User>> GetAll();
        Task<string> Authenticate(LoginModel model);
    }
}