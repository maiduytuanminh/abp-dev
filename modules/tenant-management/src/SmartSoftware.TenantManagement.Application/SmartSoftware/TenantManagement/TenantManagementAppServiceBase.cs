using SmartSoftware.Application.Services;
using SmartSoftware.TenantManagement.Localization;

namespace SmartSoftware.TenantManagement;

public abstract class TenantManagementAppServiceBase : ApplicationService
{
    protected TenantManagementAppServiceBase()
    {
        ObjectMapperContext = typeof(SmartSoftwareTenantManagementApplicationModule);
        LocalizationResource = typeof(SmartSoftwareTenantManagementResource);
    }
}
