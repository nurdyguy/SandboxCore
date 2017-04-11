using AutoMapper;
using SandboxCore.Models;
using SandboxCore.Identity.Models;
using SandboxCore.Models.AccountViewModels;

namespace SandboxCore.Mappings
{
    public class AccountProfiles : Profile
    {
        public AccountProfiles()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.Id));
        }
    }
}
