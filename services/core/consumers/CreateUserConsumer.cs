using System;
using System.Threading.Tasks;
using MassTransit;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Services;

namespace Microservices.Services.Core.Consumers
{
    public class CreateUserConsumer : IConsumer<User>
    {
        private readonly IEmailService _emailService;

        public CreateUserConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<User> context)
        {
            int emailId = await _emailService.SendEmailNewUser(context.Message);
        }
    }
}