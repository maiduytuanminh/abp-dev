using System.Threading.Tasks;
using Microsoft.JSInterop;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.Web;

public class SmartSoftwareUtilsService : ISmartSoftwareUtilsService, ITransientDependency
{
    protected IJSRuntime JsRuntime { get; }

    public SmartSoftwareUtilsService(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
    }

    public ValueTask AddClassToTagAsync(string tagName, string className)
    {
        return JsRuntime.InvokeVoidAsync("ss.utils.addClassToTag", tagName, className);
    }

    public ValueTask RemoveClassFromTagAsync(string tagName, string className)
    {
        return JsRuntime.InvokeVoidAsync("ss.utils.removeClassFromTag", tagName, className);
    }

    public ValueTask<bool> HasClassOnTagAsync(string tagName, string className)
    {
        return JsRuntime.InvokeAsync<bool>("ss.utils.hasClassOnTag", tagName, className);
    }

    public ValueTask ReplaceLinkHrefByIdAsync(string linkId, string hrefValue)
    {
        return JsRuntime.InvokeVoidAsync("ss.utils.replaceLinkHrefById", linkId, hrefValue);
    }

    public ValueTask ToggleFullscreenAsync()
    {
        return JsRuntime.InvokeVoidAsync("ss.utils.toggleFullscreen");
    }

    public ValueTask RequestFullscreenAsync()
    {
        return JsRuntime.InvokeVoidAsync("ss.utils.requestFullscreen");
    }

    public ValueTask ExitFullscreenAsync()
    {
        return JsRuntime.InvokeVoidAsync("ss.utils.exitFullscreen");
    }
}
