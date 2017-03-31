using System.Data.Common;

namespace SandboxCore.Identity.Dapper.Connections
{
    public interface IConnectionProvider
    {
        DbConnection Create();
    }
}