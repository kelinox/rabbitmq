using System.Data;
using System.Data.SqlClient;
using Microservices.Services.Core.Providers;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Microservices.Services.Infrastructure.Providers
{
    public class DbProvider : IDbProvider
    {
        private readonly IConfiguration _config;

        public DbProvider(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get { return new SqlConnection(_config.GetConnectionString("DapperConnectionString")); }
        }

        void IDbProvider.CreateDatabase()
        {
            using(IDbConnection conn = Connection)
            {
                string sql = @"CREATE TABLE Email(
                    Id int identity(1,1) not null,
                    To nvarchar(40) not null,
                    From nvarchar(40) not null,
                    Body text not null);";
                conn.ExecuteAsync(sql);
            }
        }
    }
}