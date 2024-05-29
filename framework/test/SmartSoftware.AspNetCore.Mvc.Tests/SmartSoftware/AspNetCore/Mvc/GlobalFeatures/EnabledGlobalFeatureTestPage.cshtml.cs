using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartSoftware.GlobalFeatures;

namespace SmartSoftware.AspNetCore.Mvc.GlobalFeatures;

[RequiresGlobalFeature(SmartSoftwareAspNetCoreMvcTestFeature1.Name)]
public class EnabledGlobalFeatureTestPage : PageModel
{
    public void OnGet()
    {

    }
}
