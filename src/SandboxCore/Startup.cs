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

using SandboxCore.Identity.Dapper.SqlServer;
using SandboxCore.Identity.Dapper;
using dapperSql = SandboxCore.Identity.Dapper.SqlServer;
using dapper = SandboxCore.Identity.Dapper;
//using SandboxCore.Identity.Dapper.Entities;
//using SandboxCore.Identity.Dapper.Stores;

using SandboxCore.Identity.Models;
using SandboxCore.Services;
using SandboxCore.Identity.Stores;
using SandboxCore.Identity.Repositories;
using SandboxCore.Identity.Repositories.Contracts;
using SandboxCore.Identity.Managers;
using SandboxCore.Identity;
using SandboxCore.Identity.Dapper.Stores;
using Microsoft.Extensions.Caching.Memory;
using SandboxCore.Services.Contracts;
using SandboxCore.Services.Implementations;

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
            Mappings.AutoMapperConfig.Configure();
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
            //services.AddSingleton<DapperUserStore<User, int, UserRole, RoleClaim, UserClaim, UserLogin, Role>, UserStore>();
            services.AddSingleton<IRoleStore, RoleStore>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.ConfigureDapperSqlServerConnectionProvider(Configuration.GetSection("DapperIdentity"))
                .ConfigureDapperIdentityCryptography(Configuration.GetSection("DapperIdentityCryptography"))
                .ConfigureDapperIdentityOptions(new SandboxCore.Identity.Dapper.Models.DapperIdentityOptions { UseTransactionalBehavior = true }); ;

            services.AddIdentity<User, Role>()                        
                        .AddDapperIdentityForSqlServer<int, UserRole, RoleClaim, UserClaim, UserLogin>(new SqlServerConfig())
                        .AddUserManager<UserDataService>()
                        .AddRoleManager<RoleDataService>()
                        .AddDefaultTokenProviders();

            // Add Caching Support
            services.AddMemoryCache();
            services.AddMvc();

            services.AddAuthorization(auth =>
            {
                auth.AddSecurity();
            });

            services.AddSingleton<IEulerService, EulerService>();

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
