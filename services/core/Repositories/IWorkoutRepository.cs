using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Services.Core.Entities;

namespace Microservices.Services.Core.Repositories
{
    public interface IWorkoutRepository
    {
        Task<IEnumerable<Workout>> GetAll();
    }
}