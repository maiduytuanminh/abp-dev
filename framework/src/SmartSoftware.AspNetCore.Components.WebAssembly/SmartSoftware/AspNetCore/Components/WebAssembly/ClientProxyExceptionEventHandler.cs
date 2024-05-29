using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using SmartSoftware.AspNetCore.Components.Server;
using SmartSoftware.AspNetCore.Components.Web;
using SmartSoftware.DependencyInjection;
using SmartSoftware.EventBus;
using SmartSoftware.Http;

namespace SmartSoftware.AspNetCore.Components.WebAssembly;

public class ClientProxyExceptionEventHandler : ILocalEventHandler<ClientProxyExceptionEventData>, ITransientDependency
{
    protected IServiceProvider ServiceProvider { get; }

    public ClientProxyExceptionEventHandler(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public virtual async Task HandleEventAsync(ClientProxyExceptionEventData eventData)
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            switch (eventData.StatusCode)
            {
                case 401:
                {
                    var options = scope.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareAspNetCoreComponentsWebOptions>>();
                    if (!options.Value.IsBlazorWebApp)
                    {
                        var authenticationOptions = scope.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareAuthenticationOptions>>();
                        var navigationManager = scope.ServiceProvider.GetRequiredService<NavigationManager>();
                        navigationManager.NavigateToLogout(authenticationOptions.Value.LogoutUrl, "/");
                    }
                    else
                    {
                        var jsRuntime = scope.ServiceProvider.GetRequiredService<IJSRuntime>();
                        await jsRuntime.InvokeVoidAsync("eval", "setTimeout(function(){location.assign('/')}, 2000)");
                    }

                    break;
                }
                case 403:
                {
                    var jsRuntime = scope.ServiceProvider.GetRequiredService<IJSRuntime>();
                    await jsRuntime.InvokeVoidAsync("eval", "setTimeout(function(){location.assign('/')}, 2000)");

                    break;
                }
            }
        }
    }
}
