using Microsoft.EntityFrameworkCore;
using Microservices.Service.UserApi.Models;
using Microservices.Services.Jobs.Models;

namespace Microservices.Services.Users.Infrastructure
{

    public partial class UserContext : DbContext
    {
        public UserContext() { }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }

}