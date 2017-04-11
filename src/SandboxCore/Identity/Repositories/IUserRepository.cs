using SandboxCore.Identity.Dapper.Repositories.Contracts;
using SandboxCore.Identity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SandboxCore.Identity.Repositories.Contracts
{
    public interface IUserRepository : IUserRepository<User, int, UserRole, RoleClaim, UserClaim, UserLogin, Role>
    {

        Task<IEnumerable<User>> GetAllUsersWithoutRoles();
    }
}
