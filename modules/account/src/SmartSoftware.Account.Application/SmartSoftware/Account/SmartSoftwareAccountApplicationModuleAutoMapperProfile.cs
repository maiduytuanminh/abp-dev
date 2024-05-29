using AutoMapper;
using SmartSoftware.Identity;

namespace SmartSoftware.Account;

public class SmartSoftwareAccountApplicationModuleAutoMapperProfile : Profile
{
    public SmartSoftwareAccountApplicationModuleAutoMapperProfile()
    {
        CreateMap<IdentityUser, ProfileDto>()
            .ForMember(dest => dest.HasPassword,
                op => op.MapFrom(src => src.PasswordHash != null))
            .MapExtraProperties();
    }
}
