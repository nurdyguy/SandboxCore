using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

using SandboxCore.Identity.Models;
using SandboxCore.Identity.Stores;

namespace SandboxCore.Identity.Managers
{
    public class UserDataService : UserManager<User>, IUserDataService
    {
        private readonly IUserStore _userStore;
        private readonly IOptions<IdentityOptions> _optionsAccessor;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEnumerable<IUserValidator<User>> _userValidators;
        private readonly IEnumerable<IPasswordValidator<User>> _passwordValidators;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly IdentityErrorDescriber _errors;
        private readonly IServiceProvider _services;
        private readonly ILogger<UserDataService> _logger;

        public UserDataService(
                    IUserStore userStore,
                    IOptions<IdentityOptions> optionsAccessor, 
                    IPasswordHasher<User> passwordHasher, 
                    IEnumerable<IUserValidator<User>> userValidators, 
                    IEnumerable<IPasswordValidator<User>> passwordValidators, 
                    ILookupNormalizer keyNormalizer, 
                    IdentityErrorDescriber errors, 
                    IServiceProvider services, 
                    ILogger<UserDataService> logger)
            : base(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _userStore = userStore;
            _optionsAccessor = optionsAccessor;
            _passwordHasher = passwordHasher;
            _userValidators = userValidators;
            _passwordHasher = passwordHasher;
            _keyNormalizer = keyNormalizer;
            _errors = errors;
            _services = services;
            _logger = logger;
        }
        
        public async Task<IEnumerable<User>> GetAllUsersWithoutRoles()
        {
            var users = await _userStore.GetAllUsersWithoutRoles();

            return users;
        }

    }
}
