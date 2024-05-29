using AutoMapper;

namespace SmartSoftware.Identity;

public class SmartSoftwareIdentityApplicationModuleAutoMapperProfile : Profile
{
    public SmartSoftwareIdentityApplicationModuleAutoMapperProfile()
    {
        CreateMap<IdentityUser, IdentityUserDto>()
            .MapExtraProperties();

        CreateMap<IdentityRole, IdentityRoleDto>()
            .MapExtraProperties();
    }
}
