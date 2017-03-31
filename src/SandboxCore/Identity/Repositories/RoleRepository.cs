using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using SandboxCore.Identity.Dapper.Connections;
using SandboxCore.Identity.Dapper.Models;

using SandboxCore.Identity.Dapper.Repositories;

using SandboxCore.Identity.Models;
using SandboxCore.Identity.Repositories.Contracts;


namespace SandboxCore.Identity.Repositories
{
    public class RoleRepository : RoleRepository<Role, int, UserRole, RoleClaim>, IRoleRepository
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly ILogger<RoleRepository> _log;
        private readonly SqlConfiguration _sqlConfiguration;

        public RoleRepository(IConnectionProvider connProv,
                              ILogger<RoleRepository> log,
                              SqlConfiguration sqlConf)
                : base(connProv, log, sqlConf)
        {
            _connectionProvider = connProv;
            _log = log;
            _sqlConfiguration = sqlConf;
        }

        public string getYourFace()
        {
            return "YourFace";
        }
    }

}
