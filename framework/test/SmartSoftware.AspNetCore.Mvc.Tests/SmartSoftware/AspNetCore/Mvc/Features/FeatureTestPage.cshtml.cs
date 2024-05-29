using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.Features;

namespace SmartSoftware.AspNetCore.Mvc.Features;

public class FeatureTestPage : SmartSoftwarePageModel
{
    [RequiresFeature("AllowedFeature")]
    public Task OnGetAllowedFeatureAsync()
    {
        return Task.CompletedTask;
    }

    [RequiresFeature("NotAllowedFeature")]
    public ObjectResult OnGetNotAllowedFeature()
    {
        return new ObjectResult(42);
    }

    public ObjectResult OnGetNoFeature()
    {
        return new ObjectResult(42);
    }
}
