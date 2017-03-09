using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

using Identity.Dapper.SqlServer;
using Identity.Dapper;
using Identity.Dapper.Entities;
using Identity.Dapper.Stores;

using SandboxCore.Identity.Models;
using SandboxCore.Services;
using SandboxCore.Identity.Stores;
using SandboxCore.Identity.Dapper.SqlServer;

namespace SandboxCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);


            services.AddSingleton<IUserStore, UserStore>();
            services.AddSingleton<IRoleStore, RoleStore>();

            //services.AddSingleton<IUserStore<DapperIdentityUser<int>>, DapperUserStore<DapperIdentityUser<int>, int, DapperIdentityUserRole<int>, DapperIdentityRoleClaim<int>>>();
            //services.AddSingleton<IRoleStore<DapperIdentityRole<int>>, DapperRoleStore<DapperIdentityRole<int>, int, DapperIdentityUserRole<int>, DapperIdentityRoleClaim<int>>>();

            services.ConfigureSandboxCoreDapperSqlServerConnectionProvider(Configuration.GetSection("DapperIdentity"))
                .ConfigureDapperIdentityCryptography(Configuration.GetSection("DapperIdentityCryptography"));

            services.AddIdentity<User, Role>()
                    .AddSandboxCoreDapperIdentityForSqlServer<int, UserRole, RoleClaim, UserClaim, UserLogin>()
                    .AddDefaultTokenProviders();

            //services.AddIdentity<DapperIdentityUser<int>, DapperIdentityRole<int>>()
            //        .AddDapperIdentityForSqlServer()
            //        .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
