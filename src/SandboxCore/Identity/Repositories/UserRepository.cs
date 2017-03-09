using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Identity.Dapper.Entities;
using Identity.Dapper.Connections;
using Identity.Dapper.Models;

using SandboxCore.Identity.Models;
using SandboxCore.Identity.Repositories.Contracts;

using SandboxCore.Identity.Dapper.Repositories;

namespace SandboxCore.Identity.Repositories
{
    public class UserRepository : UserRepository<User, int, UserRole, RoleClaim, UserClaim, UserLogin, Role>, IUserRepository
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly ILogger<UserRepository> _log;
        private readonly SqlConfiguration _sqlConfiguration;
        private readonly IRoleRepository _roleRepository;

        public UserRepository(IConnectionProvider connProv,
                              ILogger<UserRepository> log,
                              SqlConfiguration sqlConf,
                              IRoleRepository roleRepo
                            )
                : base(connProv, log, sqlConf, roleRepo)//  as SandboxCore.Identity.Dapper.Repositories.Contracts.IRoleRepository<DapperIdentityRole<int, UserRole, RoleClaim>, int, UserRole, RoleClaim>)
        {
            _connectionProvider = connProv;
            _log = log;
            _sqlConfiguration = sqlConf;
            _roleRepository = roleRepo;
        }

        public void yourface()
        {
            _roleRepository.GetById(1);
        }
    }
}
