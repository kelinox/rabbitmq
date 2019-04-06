using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Providers;
using Microservices.Services.Core.Repositories;

namespace Microservices.Services.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbProvider _dbProvider;

        public UserRepository(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<User> AddNewUser(User user)
        {
            string query = @"INSERT INTO [dbo].[Users](Email, Password)
                                OUTPUT INSERTED.*
                                VALUES(@Email, @Password)";
            using (IDbConnection conn = _dbProvider.Connection)
            {
                conn.Open();
                using (IDbTransaction transaction = conn.BeginTransaction())
                {
                    User res = await conn.QuerySingleAsync<User>(query, user, transaction);
                    transaction.Commit();
                    return res;
                }
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            string query = "SELECT * FROM [dbo].[Users]";
            using (IDbConnection conn = _dbProvider.Connection)
            {
                conn.Open();
                return await conn.QueryAsync<User>(query);
            }
        }
    }
}