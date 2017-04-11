using AutoMapper;
using SandboxCore.Identity.Models;
using SandboxCore.Models;
using SandboxCore.Models.AccountViewModels;


namespace SandboxCore.Mappings
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AccountProfiles>();
                cfg.AddProfile<MappingProfile<Role, RoleViewModel>>();
            });
        }
    }
}
