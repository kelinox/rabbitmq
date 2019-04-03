using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microservices.Services.Infrastructure.Entities;
using Microservices.Services.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace Microservices.Services.Core.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _config;

        public EmailRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DapperConnectionString"));
            }
        }

        public async Task<Email> AddAsync(Email email)
        {
            using(IDbConnection conn = Connection)
            {
                string sql = "INSERT INTO Email(To, From, Body) VALUES(@To, @From, @Body)";
                return await conn.ExecuteScalarAsync<Email>(sql, email);
            }
        }

        public async Task<IEnumerable<Email>> GetAllAsync()
        {
            using (IDbConnection conn = Connection)
            {
                string query = "SELECT * FROM Email";
                conn.Open();
                var result = await conn.QueryAsync<Email>(query);
                return result;
            }
        }
    }
}