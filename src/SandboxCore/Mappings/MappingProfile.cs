using AutoMapper;

namespace SandboxCore.Mappings
{
    public class MappingProfile<TSource, TDestination> : Profile
    {
        public MappingProfile()
        {
            CreateMap<TSource, TDestination>().ReverseMap();
        }
    }
}
