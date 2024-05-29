using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Demo;

public class TestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<SmartSoftwareAspNetCoreMvcUiBootstrapDemoTestModule>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
