using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware;

namespace SmartSoftware.ClientSimulation.Demo;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<ClientSimulationDemoModule>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.InitializeApplication();
    }
}
