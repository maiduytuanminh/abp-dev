using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace SmartSoftware.AspNetCore.Mvc.Localization;

[Route("api/LocalizationTestController")]
public class LocalizationTestController : SmartSoftwareController
{
    [HttpGet]
    public string Culture()
    {
        return CultureInfo.CurrentCulture.Name + ":" + CultureInfo.CurrentUICulture.Name;
    }
}
