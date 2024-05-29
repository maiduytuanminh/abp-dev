using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.AspNetCore.Components;

namespace MyCompanyName.MyProjectName;

public abstract class MyProjectNameComponentBase : SmartSoftwareComponentBase
{
    protected MyProjectNameComponentBase()
    {
        LocalizationResource = typeof(MyProjectNameResource);
    }
}
