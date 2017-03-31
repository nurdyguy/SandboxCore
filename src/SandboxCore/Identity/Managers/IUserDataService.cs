using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SandboxCore.Identity.Models;

namespace SandboxCore.Identity.Managers
{
    public interface IUserDataService
    {
        Task<IEnumerable<User>> GetAllUsersWithoutRoles();


    }
}
