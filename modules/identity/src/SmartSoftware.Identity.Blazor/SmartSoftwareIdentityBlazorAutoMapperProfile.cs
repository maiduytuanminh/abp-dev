using AutoMapper;
using SmartSoftware.AutoMapper;

namespace SmartSoftware.Identity.Blazor;

public class SmartSoftwareIdentityBlazorAutoMapperProfile : Profile
{
    public SmartSoftwareIdentityBlazorAutoMapperProfile()
    {
        CreateMap<IdentityUserDto, IdentityUserUpdateDto>()
            .MapExtraProperties()
            .Ignore(x => x.Password)
            .Ignore(x => x.RoleNames);

        CreateMap<IdentityRoleDto, IdentityRoleUpdateDto>()
            .MapExtraProperties();
    }
}
