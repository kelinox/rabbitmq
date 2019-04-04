using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Providers;
using Microservices.Services.Core.Repositories;
using Microservices.Services.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace Microservices.Services.Infrastructure.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IDbProvider _dbProvider;

        public EmailRepository(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<Email> AddAsync(Email email)
        {
            using(IDbConnection conn = _dbProvider.Connection)
            {
                string sql = "INSERT INTO [dbo].[Emails](To, From, Body) VALUES(@To, @From, @Body)";
                return await conn.ExecuteScalarAsync<Email>(sql, email);
            }
        }

        public async Task<IEnumerable<Email>> GetAllAsync()
        {
            using (IDbConnection conn = _dbProvider.Connection)
            {
                string query = "SELECT * FROM [dbo].[Emails]";
                conn.Open();
                var result = await conn.QueryAsync<Email>(query);
                return result;
            }
        }
    }
}