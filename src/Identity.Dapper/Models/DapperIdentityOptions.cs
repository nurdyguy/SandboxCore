using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SandboxCore.Identity.Dapper.Models
{
    public class DapperIdentityOptions
    {
        public bool UseTransactionalBehavior { get; set; } = false;
    }
}