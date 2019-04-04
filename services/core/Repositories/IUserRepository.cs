using System.Threading.Tasks;
using Microservices.Services.Core.Entities;

namespace Microservices.Services.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAll();
    }
}