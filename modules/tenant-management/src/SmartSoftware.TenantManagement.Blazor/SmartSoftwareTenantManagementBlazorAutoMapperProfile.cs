using AutoMapper;

namespace SmartSoftware.TenantManagement.Blazor;

public class SmartSoftwareTenantManagementBlazorAutoMapperProfile : Profile
{
    public SmartSoftwareTenantManagementBlazorAutoMapperProfile()
    {
        CreateMap<TenantDto, TenantUpdateDto>()
            .MapExtraProperties();
    }
}
