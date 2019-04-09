using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Providers;
using Microservices.Services.Core.Repositories;

namespace Microservices.Services.Infrastructure.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly IDbProvider _dbProvider;

        public WorkoutRepository(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<IEnumerable<Workout>> GetAll()
        {
            string query = "SELECT * FROM [dbo].[Workouts]";
            using(IDbConnection conn = _dbProvider.Connection)
            {
                conn.Open();
                return await conn.QueryAsync<Workout>(query);
            }
        }
    }
}