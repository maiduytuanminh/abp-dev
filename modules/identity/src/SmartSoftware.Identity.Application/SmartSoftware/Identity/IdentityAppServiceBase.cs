using SmartSoftware.Application.Services;
using SmartSoftware.Identity.Localization;

namespace SmartSoftware.Identity;

public abstract class IdentityAppServiceBase : ApplicationService
{
    protected IdentityAppServiceBase()
    {
        ObjectMapperContext = typeof(SmartSoftwareIdentityApplicationModule);
        LocalizationResource = typeof(IdentityResource);
    }
}
