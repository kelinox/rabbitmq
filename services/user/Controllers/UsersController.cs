using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MassTransit;
using Microservices.Services.Core.Controllers;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Interface.Services;
using Microservices.Services.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace user.Controllers
{
    public class UsersController : BaseController
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

        /// <summary>
        /// Register a user in the database
        /// </summary>
        /// <param name="user">The data needed to register the email</param>
        /// <returns>The newly created user</returns>
        [HttpPost("/api/register")]
        public async Task<IActionResult> Register(User user)
        {
            User res = await _userService.AddNewUser(user);
        
            Uri uri = new Uri($"rabbitmq://{_config.GetValue<string>("RabbitMQHostName")}/create_user");

            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(res);

            return Ok(Microservices.Services.Core.Models.Response<User>.Succeeded(res));
        }

        /// <summary>
        /// Authenticate a user with its password and username
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The JwtToken issued if the user is authenticate, otherwise bad request</returns>
        [HttpPost]
        [Route("/api/login")]
        public async Task<IActionResult> Authenticate(LoginModel model)
        {
            return await HandleComputationFailure(_userService.Authenticate(model));
        }
    }
}
