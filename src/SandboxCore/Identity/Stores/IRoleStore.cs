using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using SandboxCore.Identity.Models;

namespace SandboxCore.Identity.Stores
{
    public interface IRoleStore : IRoleStore<Role>, IDisposable
    {
    }
}
