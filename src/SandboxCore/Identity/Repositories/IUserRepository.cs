using SandboxCore.Identity.Dapper.Repositories.Contracts;
using SandboxCore.Identity.Models;

namespace SandboxCore.Identity.Repositories.Contracts
{
    public interface IUserRepository : IUserRepository<User, int, UserRole, RoleClaim, UserClaim, UserLogin, Role>
    {
    }
}
