using Identity.Dapper.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Identity.Dapper.Connections;

using Microsoft.Extensions.Logging;

using SandboxCore.Identity.Dapper.Stores;
using SandboxCore.Identity.Models;
using SandboxCore.Identity.Repositories.Contracts;

namespace SandboxCore.Identity.Stores
{
    public class UserStore : DapperUserStore<User, int, UserRole, RoleClaim, UserClaim, UserLogin, Role>, IUserStore                            
    {
        //private DbTransaction _transaction;
        //private DbConnection _connection;

        private readonly IConnectionProvider _connectionProvider;
        private readonly ILogger<UserStore> _log;
        private readonly IUserRepository _userRepository;

        public UserStore(IConnectionProvider connProv, 
                         ILogger<UserStore> log,
                         IUserRepository userRepo)
                : base(connProv, log, userRepo)
        {
            _connectionProvider = connProv;
            _log = log;
            _userRepository = userRepo;
        }

        //public Task<IdentityResult> CreateAsync(DapperIdentityUser user, CancellationToken cancellationToken)
        //{
        //    user.UserId = Guid.NewGuid().ToString();

        //    _users.Add(user);

        //    return Task.FromResult(IdentityResult.Success);
        //}

        //public Task<IdentityResult> UpdateAsync(DapperIdentityUser user, CancellationToken cancellationToken)
        //{
        //    var match = _users.FirstOrDefault(u => u.Id == user.Id);
        //    if (match != null)
        //    {
        //        match.UserName = user.UserName;
        //        //match.LastName = user.LastName;
        //        match.Email = user.Email;
        //        match.PhoneNumber = user.PhoneNumber;
        //        match.TwoFactorEnabled = user.TwoFactorEnabled;
        //        match.PasswordHash = user.PasswordHash;
        //        //match.TempPassword = user.TempPassword;
        //        //match.IsActive = user.IsActive;
        //        //match.RegisteredOn = user.RegisteredOn;
        //        //match.LastLoggedOn = DateTime.Now;

        //        return Task.FromResult(IdentityResult.Success);
        //    }
        //    else
        //    {
        //        return Task.FromResult(IdentityResult.Failed());
        //    }
        //}

        //public Task<IdentityResult> DeleteAsync(DapperIdentityUser user, CancellationToken cancellationToken)
        //{
        //    var match = _users.FirstOrDefault(u => u.Id == user.Id);
        //    if (match != null)
        //    {
        //        _users.Remove(match);

        //        return Task.FromResult(IdentityResult.Success);
        //    }
        //    else
        //    {
        //        return Task.FromResult(IdentityResult.Failed());
        //    }
        //}

        //public Task<DapperIdentityUser> FindByIdAsync(int userId, CancellationToken cancellationToken)
        //{
        //    var user = _users.FirstOrDefault(u => u.Id == userId);

        //    return Task.FromResult(user);
        //}

        //public Task<DapperIdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        //{
        //    var user = _users.FirstOrDefault(u => u.Email == normalizedUserName);

        //    return Task.FromResult(user);
        //}

        //public Task<int> GetUserIdAsync(DapperIdentityUser user, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(user.Id);
        //}

        //public Task<string> GetUserNameAsync(DapperIdentityUser user, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(user.Email);
        //}

        //public Task<string> GetNormalizedUserNameAsync(DapperIdentityUser user, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(user.Email);
        //}

        //public Task<string> GetPasswordHashAsync(DapperIdentityUser user, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(user.PasswordHash);
        //}

        //public Task<bool> HasPasswordAsync(DapperIdentityUser user, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(user.PasswordHash != null);
        //}

        //public Task SetUserNameAsync(DapperIdentityUser user, string userName, CancellationToken cancellationToken)
        //{
        //    user.Email = userName;
        //    return Task.FromResult(true);
        //}

        //public Task SetNormalizedUserNameAsync(DapperIdentityUser user, string normalizedName, CancellationToken cancellationToken)
        //{
        //    user.Email = normalizedName;
        //    return Task.FromResult(true);
        //}

        //public Task SetPasswordHashAsync(DapperIdentityUser user, string passwordHash, CancellationToken cancellationToken)
        //{
        //    user.PasswordHash = passwordHash;
        //    return Task.FromResult(true);
        //}

        //public Task<string> GetPhoneNumberAsync(DapperIdentityUser user, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(user.PhoneNumber);
        //}

        //public Task SetPhoneNumberAsync(DapperIdentityUser user, string phoneNumber, CancellationToken cancellationToken)
        //{
        //    user.PhoneNumber = phoneNumber;
        //    return Task.FromResult(true);
        //}

        //public Task<bool> GetPhoneNumberConfirmedAsync(DapperIdentityUser user, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(user.PhoneNumberConfirmed);
        //}

        //public Task SetPhoneNumberConfirmedAsync(DapperIdentityUser user, bool confirmed, CancellationToken cancellationToken)
        //{
        //    user.PhoneNumberConfirmed = confirmed;
        //    return Task.FromResult(true);
        //}

        //public Task SetTwoFactorEnabledAsync(DapperIdentityUser user, bool enabled, CancellationToken cancellationToken)
        //{
        //    user.TwoFactorEnabled = enabled;
        //    return Task.FromResult(true);
        //}

        //public Task<bool> GetTwoFactorEnabledAsync(DapperIdentityUser user, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(user.TwoFactorEnabled);
        //}

        //public Task<IList<UserLoginInfo>> GetLoginsAsync(DapperIdentityUser user, CancellationToken cancellationToken)
        //{
        //    // Just returning an empty list because I don't feel like implementing this. You should get the idea though...
        //    IList<UserLoginInfo> logins = new List<UserLoginInfo>();
        //    return Task.FromResult(logins);
        //}

        //public Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task AddLoginAsync(DapperIdentityUser user, UserLoginInfo login, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task RemoveLoginAsync(DapperIdentityUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Dispose() { }
    }
}
