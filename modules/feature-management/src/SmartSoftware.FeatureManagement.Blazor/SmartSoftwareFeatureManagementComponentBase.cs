using SmartSoftware.AspNetCore.Components;
using SmartSoftware.FeatureManagement.Localization;

namespace SmartSoftware.FeatureManagement.Blazor;

public abstract class SmartSoftwareFeatureManagementComponentBase : SmartSoftwareComponentBase
{
    protected SmartSoftwareFeatureManagementComponentBase()
    {
        LocalizationResource = typeof(SmartSoftwareFeatureManagementResource);
    }
}
