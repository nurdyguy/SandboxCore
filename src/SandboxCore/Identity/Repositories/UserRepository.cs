using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Common;
using System.Text;
using Microsoft.Extensions.Logging;

using Dapper;
using SandboxCore.Identity.Dapper;
using SandboxCore.Identity.Dapper.Entities;
using SandboxCore.Identity.Dapper.Connections;
using SandboxCore.Identity.Dapper.Models;

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
                : base(connProv, log, sqlConf, roleRepo)
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

        public async Task<IEnumerable<User>> GetAllUsersWithoutRoles()
        {
            try
            {
                using (var conn = _connectionProvider.Create())
                {
                    const string query = @"
                        Select t1.* 
                        From [Users] t1
                            left Join [UserRoles] t2 on t1.Id = t2.UserId 
                        Where 
                            t2.UserId is null";
                    var users = await conn.QueryAsync<User>(query);
                    return users;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(new EventId(15), ex.Message, ex);

                return null;
            }
        }

    }
}
