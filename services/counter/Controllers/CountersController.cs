using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microservices.Services.Counters.Infrastructure;
using Microservices.Services.Counters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace counter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountersController : ControllerBase
    {
        private readonly CounterContext _context;
        private readonly IBus _bus;

        public CountersController(CounterContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
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
            Console.Out.WriteLine("Enter controller");
            var number = await _context.Counters.Where(c => c.UserId == counter.UserId).MaxAsync(c => c.Number) + 1;
            counter.Number = number;
            await _context.Counters.AddAsync(counter);
            await _context.SaveChangesAsync();

            var res = await _context.Counters.FirstOrDefaultAsync(c => c.CounterId == counter.CounterId);

            var uri = new Uri("rabbitmq://localhost/counters");
            Console.Out.WriteLine(uri.AbsoluteUri);

            var endpoint = await _bus.GetSendEndpoint(uri);  //?bind=true&queue=dotnetgigs
            await endpoint.Send<Counter>(res);

            return Ok(res);
        }
    }
}
