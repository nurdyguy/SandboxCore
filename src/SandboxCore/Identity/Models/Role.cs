using Identity.Dapper.Entities;

namespace SandboxCore.Identity.Models
{
    public class Role :  DapperIdentityRole<int, UserRole, RoleClaim>
    {
    }
}
