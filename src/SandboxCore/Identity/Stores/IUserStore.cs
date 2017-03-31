using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using SandboxCore.Identity.Models;

namespace SandboxCore.Identity.Stores
{
    public interface IUserStore :   IUserStore<User>,
                                    IUserLoginStore<User>,
                                    IUserRoleStore<User>,
                                    IUserClaimStore<User>,
                                    IUserPasswordStore<User>,
                                    IUserSecurityStampStore<User>,
                                    IUserEmailStore<User>,
                                    IUserLockoutStore<User>,
                                    IUserPhoneNumberStore<User>,
                                    IQueryableUserStore<User>,
                                    IUserTwoFactorStore<User>,
                                    IUserAuthenticationTokenStore<User>
    {
        Task<IEnumerable<User>> GetAllUsersWithoutRoles();
    }
}
