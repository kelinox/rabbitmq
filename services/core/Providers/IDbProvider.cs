using System.Data;

namespace Microservices.Services.Core.Providers
{
    public interface IDbProvider
    {
        void CreateDatabase();
        IDbConnection Connection { get; }
    }
}