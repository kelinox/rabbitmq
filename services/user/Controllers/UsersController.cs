using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace user.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBusControl _bus;
        private readonly IConfiguration _config;

        public UsersController(IUserService userService, IBusControl bus, IConfiguration config)
        {
            _userService = userService;
            _bus = bus;
            _config = config;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userService.GetAll();
        }

        // POST api/values
        [HttpPost]
        public async Task<User> Post(User user)
        {
            User res = await _userService.AddNewUser(user);
        
            Uri uri = new Uri($"rabbitmq://{_config.GetValue<string>("RabbitMQHostName")}/create_user");

            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(res);

            return res;
        }
    }
}
