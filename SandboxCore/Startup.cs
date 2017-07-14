using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using AccountService.Services.Contracts;
using AccountService.Services.Implementations;
using AccountService.Repositories.Contracts;
using AccountService.Repositories.Implementations;

using SandboxCore.Authentication;
using IdentityServer4;
using MathService.Services.Contracts;
using MathService.Services.Implementations;

namespace SandboxCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Mappings.AutoMapperConfig.Configure();
            //Calculator.InitCalculator();  //------------------------------------------------
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                .AddInMemoryClients(IdentityConfig.GetClients())
                .AddTestUsers(IdentityConfig.GetUsers());

            services.AddAuthorization(auth =>
            {
                auth.AddSecurity();
            });

            services.AddSingleton<IUserDataService, UserDataService>();

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserRoleRepository, UserRoleRepository>();

            services.AddSingleton<IEulerService, EulerService>();

            Action<AccountService.AccountServiceOptions> options = (opt =>
            {
                opt.AppDBConnection = Configuration["ConnectionStrings:DefaultConnection"];
            });
            services.Configure(options);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AccountService.AccountServiceOptions>>().Value);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIdentityServer();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                AutomaticAuthenticate = false,
                AutomaticChallenge = false
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = SandboxCore.Authentication.AuthenticationOptions.AuthScheme,
                AutomaticAuthenticate = false,
                AutomaticChallenge = false
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
