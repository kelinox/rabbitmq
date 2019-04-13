using System;
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
        
        /// <summary>
        /// List all the emails in tha database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Email>> GetAllAsync()
        {
            string query = "SELECT * FROM [dbo].[Emails]";
            using (IDbConnection conn = _dbProvider.Connection)
            {
                conn.Open();
                var result = await conn.QueryAsync<Email>(query);
                return result;
            }
        }

        /// <summary>
        /// Send a welcome email to a user when he register on the website
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The id of the email sent</returns>
        public async Task<int> SendEmailNewUser(User user)
        {
            string query = @"INSERT INTO [dbo].[Emails]([From], [To], [Body], [UserId])
                                VALUES(@To, @From, @Body, @UserId)
                                SELECT CAST(SCOPE_IDENTITY() AS INT)";
            using (IDbConnection conn = _dbProvider.Connection)
            {
                conn.Open();
                return await conn.QueryFirstAsync<int>(query, new
                {
                    To = user.Email,
                    From = "test",
                    Body = "test body",
                    UserId = user.Id
                });

            }
        }
    }
}