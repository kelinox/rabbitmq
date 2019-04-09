using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Interface.Services;
using Microservices.Services.Core.Repositories;

namespace Microservices.Services.Core.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;

        public WorkoutService(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public async Task<IEnumerable<Workout>> GetAll()
        {
            return await _workoutRepository.GetAll();
        }
    }
}