using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using SandboxCore.Identity.Models;
using SandboxCore.Identity.Stores;


namespace SandboxCore.Identity.Managers
{
    public class RoleDataService : RoleManager<Role>, IRoleDataService
    {
        public RoleDataService(
                        IRoleStore store, 
                        IEnumerable<IRoleValidator<Role>> roleValidators, 
                        ILookupNormalizer keyNormalizer, 
                        IdentityErrorDescriber errors, 
                        ILogger<RoleDataService> logger, 
                        IHttpContextAccessor contextAccessor)
            : base(store, roleValidators, keyNormalizer, errors, logger, contextAccessor)
        {

        }

    }
}
