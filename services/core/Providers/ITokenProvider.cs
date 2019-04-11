using Microservices.Services.Core.Entities;

namespace Microservices.Services.Core.Providers
{
    public interface ITokenProvider
    {
        string GetToken(User user);
    }
}