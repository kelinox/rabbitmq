using System.Threading.Tasks;
using Microservices.Services.Infrastructure.Entities;

namespace Microservices.Services.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAll();
    }
}