using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.AspNetCore.Components;

namespace MyCompanyName.MyProjectName.Blazor.WebApp.Client;

public abstract class MyProjectNameComponentBase : SmartSoftwareComponentBase
{
    protected MyProjectNameComponentBase()
    {
        LocalizationResource = typeof(MyProjectNameResource);
    }
}
