using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.AspNetCore.Components;

namespace MyCompanyName.MyProjectName.Blazor.WebApp;

public abstract class MyProjectNameComponentBase : SmartSoftwareComponentBase
{
    protected MyProjectNameComponentBase()
    {
        LocalizationResource = typeof(MyProjectNameResource);
    }
}
