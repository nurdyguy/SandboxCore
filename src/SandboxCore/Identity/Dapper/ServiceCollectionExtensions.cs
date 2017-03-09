using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Identity.Dapper.Connections;
using Identity.Dapper.Cryptography;
using Identity.Dapper.Entities;
using Identity.Dapper.Models;

using SandboxCore.Identity.Dapper.Repositories;
using SandboxCore.Identity.Dapper.Repositories.Contracts;
using SandboxCore.Identity.Dapper.Stores;

using Identity.Dapper.SqlServer.Connections;
using Identity.Dapper.SqlServer.Models;





namespace SandboxCore.Identity.Dapper.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        public static IdentityBuilder AddSandboxCoreDapperIdentityForSqlServer(this IdentityBuilder builder)
        {
            builder.Services.AddSingleton<SqlConfiguration, SqlServerConfiguration>();

            AddSandboxCoreStores(builder.Services, builder.UserType, builder.RoleType);

            return builder;
        }

        public static IdentityBuilder AddSandboxCoreDapperIdentityForSqlServer(this IdentityBuilder builder, SqlServerConfiguration configurationOverride)
        {
            builder.Services.AddSingleton<SqlConfiguration>(configurationOverride);

            AddSandboxCoreStores(builder.Services, builder.UserType, builder.RoleType);

            return builder;
        }

        public static IdentityBuilder AddSandboxCoreDapperIdentityForSqlServer<TKey>(this IdentityBuilder builder)
        {
            builder.Services.AddSingleton<SqlConfiguration, SqlServerConfiguration>();

            AddSandboxCoreStores(builder.Services, builder.UserType, builder.RoleType, typeof(TKey));

            return builder;
        }

        public static IdentityBuilder AddSandboxCoreDapperIdentityForSqlServer<TKey>(this IdentityBuilder builder, SqlServerConfiguration configurationOverride)
        {
            builder.Services.AddSingleton<SqlConfiguration>(configurationOverride);

            AddSandboxCoreStores(builder.Services, builder.UserType, builder.RoleType, typeof(TKey));

            return builder;
        }

        public static IdentityBuilder AddSandboxCoreDapperIdentityForSqlServer<TKey, TUserRole, TRoleClaim>(this IdentityBuilder builder)
        {
            builder.Services.AddSingleton<SqlConfiguration, SqlServerConfiguration>();

            AddSandboxCoreStores(builder.Services, builder.UserType, builder.RoleType, typeof(TKey), typeof(TUserRole), typeof(TRoleClaim));

            return builder;
        }

        public static IdentityBuilder AddSandboxCoreDapperIdentityForSqlServer<TKey, TUserRole, TRoleClaim>(this IdentityBuilder builder, SqlServerConfiguration configurationOverride)
        {
            builder.Services.AddSingleton<SqlConfiguration>(configurationOverride);

            AddSandboxCoreStores(builder.Services, builder.UserType, builder.RoleType, typeof(TKey), typeof(TUserRole), typeof(TRoleClaim));

            return builder;
        }

        public static IdentityBuilder AddSandboxCoreDapperIdentityForSqlServer<TKey, TUserRole, TRoleClaim, TUserClaim, TUserLogin>(this IdentityBuilder builder)
        {
            builder.Services.AddSingleton<SqlConfiguration, SqlServerConfiguration>();

            AddSandboxCoreStores(builder.Services, builder.UserType, builder.RoleType, typeof(TKey), typeof(TUserRole), typeof(TRoleClaim), typeof(TUserClaim), typeof(TUserLogin));

            return builder;
        }

        public static IdentityBuilder AddSandboxCoreDapperIdentityForSqlServer<TKey, TUserRole, TRoleClaim, TUserClaim, TUserLogin>(this IdentityBuilder builder, SqlServerConfiguration configurationOverride)
        {
            builder.Services.AddSingleton<SqlConfiguration>(configurationOverride);

            AddSandboxCoreStores(builder.Services, builder.UserType, builder.RoleType, typeof(TKey), typeof(TUserRole), typeof(TRoleClaim), typeof(TUserClaim), typeof(TUserLogin));

            return builder;
        }

        public static IdentityBuilder AddSandboxCoreDapperIdentityForSqlServer<TKey, TUserRole, TRoleClaim, TUserClaim, TUserLogin, TRole>(this IdentityBuilder builder)
        {
            builder.Services.AddSingleton<SqlConfiguration, SqlServerConfiguration>();

            AddSandboxCoreStores(builder.Services, builder.UserType, builder.RoleType, typeof(TKey), typeof(TUserRole), typeof(TRoleClaim), typeof(TUserClaim), typeof(TUserLogin));

            return builder;
        }

        public static IdentityBuilder AddSandboxCoreDapperIdentityForSqlServer<TKey, TUserRole, TRoleClaim, TUserClaim, TUserLogin, TRole>(this IdentityBuilder builder, SqlServerConfiguration configurationOverride)
        {
            builder.Services.AddSingleton<SqlConfiguration>(configurationOverride);

            AddSandboxCoreStores(builder.Services, builder.UserType, builder.RoleType, typeof(TKey), typeof(TUserRole), typeof(TRoleClaim), typeof(TUserClaim), typeof(TUserLogin));

            return builder;
        }

        private static void AddSandboxCoreStores(IServiceCollection services, Type userType, Type roleType, Type keyType = null, Type userRoleType = null, Type roleClaimType = null, Type userClaimType = null, Type userLoginType = null)
        {
            Type userStoreType;
            Type roleStoreType;
            keyType = keyType ?? typeof(int);
            userRoleType = userRoleType ?? typeof(DapperIdentityUserRole<>).MakeGenericType(keyType);
            roleClaimType = roleClaimType ?? typeof(DapperIdentityRoleClaim<>).MakeGenericType(keyType);

            userStoreType = typeof(DapperUserStore<,,,,,,>).MakeGenericType(userType, keyType, userRoleType, roleClaimType, userClaimType, userLoginType, roleType);
            roleStoreType = typeof(DapperRoleStore<,,,>).MakeGenericType(roleType, keyType, userRoleType, roleClaimType);

            services.AddScoped(typeof(IRoleRepository<,,,>).MakeGenericType(roleType, keyType, userRoleType, roleClaimType),
                               typeof(RoleRepository<,,,>).MakeGenericType(roleType, keyType, userRoleType, roleClaimType));

            services.AddScoped(typeof(IUserRepository<,,,,,,>).MakeGenericType(userType, keyType, userRoleType, roleClaimType, userClaimType, userLoginType, roleType),
                               typeof(UserRepository<,,,,,,>).MakeGenericType(userType, keyType, userRoleType, roleClaimType, userClaimType, userLoginType, roleType));

            services.AddScoped(typeof(IUserStore<>).MakeGenericType(userType), userStoreType);
            services.AddScoped(typeof(IRoleStore<>).MakeGenericType(roleType), roleStoreType);
        }

        public static IServiceCollection ConfigureSandboxCoreDapperSqlServerConnectionProvider(this IServiceCollection services, IConfigurationSection configuration)
        {
            services.Configure<ConnectionProviderOptions>(configuration);

            services.AddSingleton<IConnectionProvider, SqlServerConnectionProvider>();

            return services;
        }
    }
}