using System.Threading.Tasks;

namespace Microservices.Services.Core.Repositories
{
    public interface IAuthRepository
    {
        Task<string> Authenticate(string username, string password);
    }
}