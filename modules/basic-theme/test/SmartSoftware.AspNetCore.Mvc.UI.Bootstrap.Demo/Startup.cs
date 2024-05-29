using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Demo;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<SmartSoftwareAspNetCoreMvcUiBootstrapDemoModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
