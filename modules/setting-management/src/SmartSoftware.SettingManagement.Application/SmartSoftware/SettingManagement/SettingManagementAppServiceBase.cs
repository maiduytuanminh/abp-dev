using SmartSoftware.Application.Services;
using SmartSoftware.SettingManagement.Localization;

namespace SmartSoftware.SettingManagement;

public abstract class SettingManagementAppServiceBase : ApplicationService
{
    protected SettingManagementAppServiceBase()
    {
        ObjectMapperContext = typeof(SmartSoftwareSettingManagementApplicationModule);
        LocalizationResource = typeof(SmartSoftwareSettingManagementResource);
    }
}
