using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SandboxCore.Authentication
{
    public class AuthenticationClaims
    {
        public const string UserIdClaim = "http://identity.sandboxcore.com/userid";
        public const string UserNameClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public const string NameClaim = "http://identity.sandboxcore.com/name";
        public const string EmailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public const string IsStaffClaim = "http://identity.sandboxcore.com/isstaff";
        public const string RoleClaim = "http://identity.sandboxcore.com/role";
    }
}
