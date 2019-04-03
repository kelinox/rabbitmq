using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Services.Infrastructure.Entities;

namespace Microservices.Services.Infrastructure.Repositories
{
    public interface IEmailRepository
    {
        Task<IEnumerable<Email>> GetAllAsync();
        Task<Email> AddAsync(Email email);
    }
}