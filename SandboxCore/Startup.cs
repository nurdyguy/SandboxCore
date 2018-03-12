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
using Microsoft.AspNetCore.Http;
using IdentityServer4;
using React.AspNet;

using AccountService.Services.Contracts;
using AccountService.Services.Implementations;
using AccountService.Repositories.Contracts;
using AccountService.Repositories.Implementations;
using MathService.Services.Contracts;
using MathService.Services.Implementations;

using SandboxCore.Authentication;
using SandboxCore.Middleware;
using SandboxCore.Helpers;
using MathService.Calculators;
using MathService.Repositories.Contracts;
using MathService.Repositories.Implementations;

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
            services.AddSingleton<ICalculator, Calculator>();
            services.AddSingleton<IPrimeRepository, PrimeRepository>();

            //services.AddSingleton<ChatSocketManager>();

            Action<AccountService.AccountServiceOptions> options = (opt =>
            {
                opt.AppDBConnection = Configuration["ConnectionStrings:DefaultConnection"];
            });
            services.Configure(options);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AccountService.AccountServiceOptions>>().Value);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();

            //services.AddCors(corsOptions =>
            //{
            //    corsOptions.AddPolicy("cors", builder =>
            //        builder
            //            .AllowAnyMethod()
            //            //.WithOrigins("http://localhost:5002")                        
            //            .AllowAnyOrigin()
            //            .AllowAnyHeader()
            //            .AllowCredentials()
            //    );
            //});

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
                //app.UseBrowserLink();
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

            // Initialise ReactJS.NET. Must be before static files.
            app.UseReact(config =>
            {
                // If you want to use server-side rendering of React components,
                // add all the necessary JavaScript files here. This includes
                // your components as well as all of their dependencies.
                // See http://reactjs.net/ for more information. Example:
                //config
                //    .AddScript("~/Scripts/First.jsx")
                //    .AddScript("~/Scripts/Second.jsx");

                // If you use an external build too (for example, Babel, Webpack,
                // Browserify or Gulp), you can improve performance by disabling
                // ReactJS.NET's version of Babel and loading the pre-transpiled
                // scripts. Example:
                //config
                //    .SetLoadBabel(false)
                //    .AddScriptWithoutTransform("~/Scripts/bundle.server.js");
            });

            app.UseStaticFiles();

            //app.UseWebSockets();
            //app.UseMiddleware<ChatWebSocketMiddleware>();

            //app.UseCors("cors");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
