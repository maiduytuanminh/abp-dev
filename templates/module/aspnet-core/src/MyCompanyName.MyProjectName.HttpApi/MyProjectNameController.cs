using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.AspNetCore.Mvc;

namespace MyCompanyName.MyProjectName;

public abstract class MyProjectNameController : SmartSoftwareControllerBase
{
    protected MyProjectNameController()
    {
        LocalizationResource = typeof(MyProjectNameResource);
    }
}
