using SandboxCore.Identity.Dapper.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using SandboxCore.Identity.Dapper.Connections;
using Microsoft.Extensions.Logging;

using SandboxCore.Identity.Dapper.Stores;
using SandboxCore.Identity.Models;
using SandboxCore.Identity.Repositories.Contracts;

namespace SandboxCore.Identity.Stores
{
    public class RoleStore : DapperRoleStore<Role, int, UserRole, RoleClaim>, IRoleStore
    {
        private readonly IConnectionProvider _connectinProvider;
        private readonly ILogger<RoleStore> _log;
        private readonly IRoleRepository _roleRepository;
        private readonly SandboxCore.Identity.Dapper.UnitOfWork.Contracts.IUnitOfWork _unitOfWork;
        private readonly SandboxCore.Identity.Dapper.Models.DapperIdentityOptions _dapperIdentityOptions;

        public RoleStore(IConnectionProvider connProv, 
                         ILogger<RoleStore> log, 
                         IRoleRepository roleRepo,
                         SandboxCore.Identity.Dapper.UnitOfWork.Contracts.IUnitOfWork uow,
                         SandboxCore.Identity.Dapper.Models.DapperIdentityOptions dapperIdOpts)
            : base(connProv, log, roleRepo, uow, dapperIdOpts)
        {
            _connectinProvider = connProv;
            _log = log;
            _roleRepository = roleRepo;
            _unitOfWork = uow;
            _dapperIdentityOptions = dapperIdOpts;
        }



        //public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        //{
        //    _roles.Add(role);

        //    return Task.FromResult(IdentityResult.Success);
        //}

        //public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        //{
        //    var match = _roles.FirstOrDefault(r => r.RoleId == role.RoleId);
        //    if (match != null)
        //    {
        //        match.RoleName = role.RoleName;

        //        return Task.FromResult(IdentityResult.Success);
        //    }
        //    else
        //    {
        //        return Task.FromResult(IdentityResult.Failed());
        //    }
        //}

        //public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        //{
        //    var match = _roles.FirstOrDefault(r => r.RoleId == role.RoleId);
        //    if (match != null)
        //    {
        //        _roles.Remove(match);

        //        return Task.FromResult(IdentityResult.Success);
        //    }
        //    else
        //    {
        //        return Task.FromResult(IdentityResult.Failed());
        //    }
        //}

        //public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        //{
        //    var role = _roles.FirstOrDefault(r => r.RoleId == roleId);

        //    return Task.FromResult(role);
        //}

        //public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        //{
        //    var role = _roles.FirstOrDefault(r => r.RoleName == normalizedRoleName);

        //    return Task.FromResult(role);
        //}

        //public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(role.RoleId);
        //}

        //public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(role.RoleName);
        //}

        //public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(role.RoleName);
        //}

        //public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        //{
        //    role.RoleName = roleName;

        //    return Task.FromResult(true);
        //}

        //public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        //{
        //    role.RoleName = normalizedName;

        //    return Task.FromResult(true);
        //}

        public void Dispose() { }
    }
}
