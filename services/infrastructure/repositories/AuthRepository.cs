using System;
using System.Data;
using System.Security.Authentication;
using System.Threading.Tasks;
using Dapper;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Providers;
using Microservices.Services.Core.Repositories;

namespace Microservices.Services.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbProvider _dbProvider;
        private readonly ITokenProvider _tokenProvider;

        public AuthRepository(IDbProvider dbProvider, ITokenProvider tokenProvider)
        {
            _dbProvider = dbProvider;
            _tokenProvider = tokenProvider;
        }

        /// <summary>
        /// Authenticate a user with its username and password
        /// And return a JwtToken if the user had been authenticated
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>The JwtToken of the user, empty if user is not authenticated</returns>
        public async Task<string> Authenticate(string username, string password)
        {
            string query = @"SELECT * FROM [dbo].[Users] WHERE Username = @Username AND Password = @Password";
            using (IDbConnection conn = _dbProvider.Connection)
            {
                var user = await conn.QuerySingleOrDefaultAsync<User>(query, new { Username = username, Password = password });
                if (user is null)
                {
                    throw new AuthenticationException("Incorrect login or password");
                }
                return _tokenProvider.GetToken(user);
            }
        }
    }
}