using SandboxCore.Identity.Dapper.Repositories.Contracts;
using SandboxCore.Identity.Models;

namespace SandboxCore.Identity.Repositories.Contracts
{
    public interface IRoleRepository : IRoleRepository<Role, int, UserRole, RoleClaim>
    {

        string getYourFace();
        //entities.DapperIdentityRole<int, UserRole, RoleClaim>
        //DapperIdentityRole<int, UserRole, RoleClaim>
    }
}
