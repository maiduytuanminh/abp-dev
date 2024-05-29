using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;

namespace SmartSoftware.AspNetCore.Components.Server.Theming.Bundling;

public class BlazorGlobalScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareAspNetCoreComponentsWebOptions>>().Value;
        if (!options.IsBlazorWebApp)
        {
            context.Files.AddIfNotContains("/_framework/blazor.server.js");
        }
        context.Files.AddIfNotContains("/_content/SmartSoftware.AspNetCore.Components.Web/libs/smartsoftware/js/ss.js");
        context.Files.AddIfNotContains("/_content/SmartSoftware.AspNetCore.Components.Web/libs/smartsoftware/js/authentication-state-listener.js");
    }
}
