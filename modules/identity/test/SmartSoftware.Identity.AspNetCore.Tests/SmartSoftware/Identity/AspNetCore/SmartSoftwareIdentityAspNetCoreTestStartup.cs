using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SmartSoftware.Identity.AspNetCore;

public class SmartSoftwareIdentityAspNetCoreTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<SmartSoftwareIdentityAspNetCoreTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
