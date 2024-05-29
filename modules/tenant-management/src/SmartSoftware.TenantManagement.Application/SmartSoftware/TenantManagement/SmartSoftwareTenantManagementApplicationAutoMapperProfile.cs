using AutoMapper;

namespace SmartSoftware.TenantManagement;

public class SmartSoftwareTenantManagementApplicationAutoMapperProfile : Profile
{
    public SmartSoftwareTenantManagementApplicationAutoMapperProfile()
    {
        CreateMap<Tenant, TenantDto>()
            .MapExtraProperties();
    }
}
