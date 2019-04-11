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
            string queries = @" SELECT * FROM [dbo].[Users]
                                WHERE Username = @Username;

                                SELECT * FROM [dbo].[Users]
                                WHERE Email = @EMail;

                                INSERT INTO [dbo].[Users](Email, Password, Username)
                                OUTPUT INSERTED.*
                                VALUES(@Email, @Password, @Username)";

            using (IDbConnection conn = _dbProvider.Connection)
            {
                conn.Open();
                using (IDbTransaction transaction = conn.BeginTransaction())
                {
                    using (var multi = conn.QueryMultiple(queries, user, transaction))
                    {   
                        User res = await multi.ReadSingleOrDefaultAsync<User>();
                        if(!(res is null)) {
                            throw new DuplicateNameException("Username already exists");
                        }
                        res = await multi.ReadSingleOrDefaultAsync<User>();
                        if(!(res is null)) {
                            throw new DuplicateNameException("Email already exists");                            
                        }
                        res = await multi.ReadSingleAsync<User>();
                        transaction.Commit();
                        return res;
                    }
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