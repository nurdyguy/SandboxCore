using Microsoft.AspNetCore.Authorization;
using SandboxCore.Authentication;

namespace SandboxCore
{
    public static class StartupAuthorization
    {
        public static void AddSecurity(this AuthorizationOptions auth)
        {
            auth.AddPolicy("Admin", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Admin", "Owner");
            });

            auth.AddPolicy("Owner", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Owner", "Super_Admin");
            });

            auth.AddPolicy("User", policy =>
            {
                policy.RequireAuthenticatedUser();
                //policy.RequireClaim("User", "Admin", "Owner");
            });

            auth.AddPolicy("Ahl", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("AHL");
            });
        }
    }
}
