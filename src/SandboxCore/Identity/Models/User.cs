using Identity.Dapper.Entities;

namespace SandboxCore.Identity.Models
{
    // Add profile data for application users by adding properties to the User class
    public class User : DapperIdentityUser<int, UserClaim, UserRole, UserLogin>
    {
        
    }

    
}
