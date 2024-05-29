using AutoMapper;
using SmartSoftware.Data;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.TenantManagement;

public class SmartSoftwareTenantManagementDomainMappingProfile : Profile
{
    public SmartSoftwareTenantManagementDomainMappingProfile()
    {
        CreateMap<Tenant, TenantConfiguration>()
            .ForMember(ti => ti.ConnectionStrings, opts =>
            {
                opts.MapFrom((tenant, ti) =>
                {
                    var connStrings = new ConnectionStrings();

                    if (tenant.ConnectionStrings == null)
                    {
                        return connStrings;
                    }

                    foreach (var connectionString in tenant.ConnectionStrings)
                    {
                        connStrings[connectionString.Name] = connectionString.Value;
                    }

                    return connStrings;
                });
            })
            .ForMember(x => x.IsActive, x => x.Ignore());

        CreateMap<Tenant, TenantEto>();
    }
}
