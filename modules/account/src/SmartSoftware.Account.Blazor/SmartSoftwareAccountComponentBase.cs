using SmartSoftware.Account.Localization;
using SmartSoftware.AspNetCore.Components;

namespace SmartSoftware.Account.Blazor;

public abstract class SmartSoftwareAccountComponentBase : SmartSoftwareComponentBase
{
    protected SmartSoftwareAccountComponentBase()
    {
        LocalizationResource = typeof(AccountResource);
        ObjectMapperContext = typeof(SmartSoftwareAccountBlazorModule);
    }
}
