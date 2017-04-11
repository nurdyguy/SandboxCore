using SandboxCore.Identity.Dapper.SqlServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SandboxCore.Identity
{
    public class SqlServerConfig : SqlServerConfiguration
    {
        public SqlServerConfig()
        {
            //--- tables
            UserTable = "Users";
            RoleTable = "Roles";
            UserRoleTable = "UserRoles";
            UserClaimTable = "UserClaims";
            string RoleClaims = "RoleClaims";
            UserLoginTable = "UserLogins";
            
        }
    }
}
