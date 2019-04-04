using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Repositories;

namespace Microservices.Services.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailRepository;

        public EmailService(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public async Task<IEnumerable<Email>> GetAllAsync()
        {
            return await _emailRepository.GetAllAsync();
        }
    }
}