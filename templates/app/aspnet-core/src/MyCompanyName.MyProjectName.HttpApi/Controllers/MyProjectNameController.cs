using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.AspNetCore.Mvc;

namespace MyCompanyName.MyProjectName.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MyProjectNameController : SmartSoftwareControllerBase
{
    protected MyProjectNameController()
    {
        LocalizationResource = typeof(MyProjectNameResource);
    }
}
