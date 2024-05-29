using SmartSoftware.Application.Services;
using SmartSoftware.FeatureManagement.Localization;

namespace SmartSoftware.FeatureManagement;

public abstract class FeatureManagementAppServiceBase : ApplicationService
{
    protected FeatureManagementAppServiceBase()
    {
        ObjectMapperContext = typeof(SmartSoftwareFeatureManagementApplicationModule);
        LocalizationResource = typeof(SmartSoftwareFeatureManagementResource);
    }
}
