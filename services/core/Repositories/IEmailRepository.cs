using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Services.Core.Entities;

namespace Microservices.Services.Core.Repositories
{
    public interface IEmailRepository
    {
        Task<IEnumerable<Email>> GetAllAsync();
        Task<int> SendEmailNewUser(User user);
    }
}