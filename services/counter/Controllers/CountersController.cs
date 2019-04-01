using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microservices.Services.Counters.Infrastructure;
using Microservices.Services.Counters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace counter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountersController : ControllerBase
    {
        private readonly CounterContext _context;
        private readonly IBus _bus;
        private readonly IConfiguration _config;

        public CountersController(CounterContext context, IBus bus, IConfiguration config)
        {
            _context = context;
            _bus = bus;
            _config = config;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        { 
            return Ok(await _context.Counters.ToListAsync());
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Counter counter)
        {
            var number = await _context.Counters.Where(c => c.UserId == counter.UserId).MaxAsync(c => c.Number) + 1;
            counter.Number = number;
            await _context.Counters.AddAsync(counter);
            await _context.SaveChangesAsync();

            var res = await _context.Counters.FirstOrDefaultAsync(c => c.CounterId == counter.CounterId);

            var url = new Uri($"rabbitmq://{_config.GetValue<string>("RabbitMQHostName")}/counter");

            Console.Out.WriteLine(url);

            var endpoint = await _bus.GetSendEndpoint(url);
            await endpoint.Send<Counter>(res);

            return Ok(res);
        }
    }
}
