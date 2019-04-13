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

        /// <summary>
        /// Get the list of all the workouts in the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Workout>> GetAll()
        {
            string query = "SELECT * FROM [dbo].[Workouts]";
            using (IDbConnection conn = _dbProvider.Connection)
            {
                conn.Open();
                return await conn.QueryAsync<Workout>(query);
            }
        }

        /// <summary>
        /// Get the workouts for a userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Workout>> GetByUser(int userId)
        {
            string query = "SELECT * FROM [dbo].[Workouts] WHERE UserId = @UserId";
            using (IDbConnection conn = _dbProvider.Connection)
            {
                conn.Open();
                return await conn.QueryAsync<Workout>(query, new { UserId = userId });
            }
        }

        public async Task<Workout> GetById(int workoutId)
        {
            string query = "SELECT * FROM [dbo].[Workouts] WHERE Id = @Id";
            using (IDbConnection conn = _dbProvider.Connection)
            {
                conn.Open();
                return await conn.QuerySingleAsync<Workout>(query, new { Id = workoutId });
            }
        }
    }
}