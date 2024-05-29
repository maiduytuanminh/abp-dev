using System.Threading.Tasks;
using Microsoft.JSInterop;
using SmartSoftware.AspNetCore.Components.BlockUi;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.Web.BlockUi;

[Dependency(ReplaceServices = true)]
public class SmartSoftwareBlockUiService : IBlockUiService, IScopedDependency
{
    public IJSRuntime JsRuntime { get; }

    public SmartSoftwareBlockUiService(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
    }

    public async Task Block(string? selectors, bool busy = false)
    {
        await JsRuntime.InvokeVoidAsync("ss.ui.block", selectors, busy);
    }

    public async Task UnBlock()
    {
        await JsRuntime.InvokeVoidAsync("ss.ui.unblock");
    }
}
