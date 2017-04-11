﻿using SandboxCore.Identity.Dapper.Cryptography;
using SandboxCore.Identity.Dapper.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SandboxCore.Identity.Dapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDapperIdentityCryptography(this IServiceCollection services, IConfigurationSection configuration)
        {
            services.Configure<AESKeys>(configuration);
            services.AddSingleton<EncryptionHelper>();

            return services;
        }

        public static IServiceCollection ConfigureDapperIdentityOptions(this IServiceCollection services, DapperIdentityOptions options)
        {
            services.AddSingleton(options);
 
            return services;
        }
    }
}
