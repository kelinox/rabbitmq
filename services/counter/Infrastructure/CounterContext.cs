using Microsoft.EntityFrameworkCore;
using Microservices.Services.Counters.Models;

namespace Microservices.Services.Counters.Infrastructure
{

    public partial class CounterContext : DbContext
    {
        public CounterContext() { }

        public CounterContext(DbContextOptions<CounterContext> options) : base(options)
        { }

        public virtual DbSet<Counter> Counters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }

}