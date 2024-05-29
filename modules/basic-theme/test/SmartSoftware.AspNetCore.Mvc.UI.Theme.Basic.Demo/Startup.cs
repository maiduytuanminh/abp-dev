using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic.Demo;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<SmartSoftwareAspNetCoreMvcUiThemeBasicDemoModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
