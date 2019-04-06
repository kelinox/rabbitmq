using System.Data;

namespace Microservices.Services.Core.Providers
{
    public interface IDbProvider
    {
        IDbConnection Connection { get; }
    }
}