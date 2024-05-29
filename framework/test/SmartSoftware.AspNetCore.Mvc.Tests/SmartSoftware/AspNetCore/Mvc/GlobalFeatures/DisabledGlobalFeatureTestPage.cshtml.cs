using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartSoftware.GlobalFeatures;

namespace SmartSoftware.AspNetCore.Mvc.GlobalFeatures;

[RequiresGlobalFeature("Not-Enabled-Feature")]
public class DisabledGlobalFeatureTestPage : PageModel
{
    public void OnGet()
    {

    }
}
