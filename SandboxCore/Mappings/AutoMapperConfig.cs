using AutoMapper;



namespace SandboxCore.Mappings
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile<SandboxCore.Models.AccountViewModels.UserViewModel, AccountService.Models.User>>();
                cfg.AddProfile<MappingProfile<SandboxCore.Models.AccountViewModels.RoleViewModel, AccountService.Models.Role>>();
            });
        }
    }
}
