using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.VirtualFileSystem;
using SmartSoftware.AspNetCore.WebClientInfo;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore;

public class SmartSoftwareAspNetCoreAbstractionsModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IWebContentFileProvider, NullWebContentFileProvider>();
        context.Services.AddSingleton<IWebClientInfoProvider, NullWebClientInfoProvider>();;
    }
}
