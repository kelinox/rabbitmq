using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Services.Core.Entities;

namespace Microservices.Services.Core.Interface.Services
{
    public interface IWorkoutService 
    {
        Task<IEnumerable<Workout>> GetAll();
        Task<IEnumerable<Workout>> GetByUser(int userId);
    }
}