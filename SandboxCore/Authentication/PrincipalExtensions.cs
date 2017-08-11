﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using SandboxCore.Authentication;

namespace SandboxCore.Authentication
{
    public static class PrincipalExtensions
    {   
        public static bool IsOwner(this ClaimsPrincipal principal)
        {
            return principal.HasClaim(x => x.Type == JwtClaimTypes.Role && (x.Value == "Owner"));
        }
        
        public static bool IsAdmin(this ClaimsPrincipal principal)
        {
            return principal.HasClaim(x => x.Type == JwtClaimTypes.Role && (x.Value == "Owner" || x.Value == "Admin"));
        }
        
        public static int UserId(this ClaimsPrincipal principal)
        {
            var value = principal.HasClaim(x => x.Type == AuthenticationClaims.UserIdClaim) ? principal.FindFirst(AuthenticationClaims.UserIdClaim).Value : null;
            return string.IsNullOrWhiteSpace(value) ? 0 : int.Parse(value);
        }
        
        public static string UserName(this ClaimsPrincipal principal)
        {
            var value = principal.HasClaim(x => x.Type == AuthenticationClaims.UserNameClaim) ? principal.FindFirst(AuthenticationClaims.UserNameClaim).Value : null;
            return value;
        }
        
        public static string Name(this ClaimsPrincipal principal)
        {
            var value = principal.HasClaim(x => x.Type == AuthenticationClaims.NameClaim) ? principal.FindFirst(AuthenticationClaims.NameClaim).Value : null;
            return value;
        }

        public static bool IsUser(this ClaimsPrincipal principal)
        {
            return principal.Identity.IsAuthenticated;
        }
    }
}
