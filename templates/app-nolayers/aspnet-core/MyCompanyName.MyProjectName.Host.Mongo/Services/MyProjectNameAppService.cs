using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.Application.Services;

namespace MyCompanyName.MyProjectName.Services;

/* Inherit your application services from this class. */
public abstract class MyProjectNameAppService : ApplicationService
{
    protected MyProjectNameAppService()
    {
        LocalizationResource = typeof(MyProjectNameResource);
    }
}