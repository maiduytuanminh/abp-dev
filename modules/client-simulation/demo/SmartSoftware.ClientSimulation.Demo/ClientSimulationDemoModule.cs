using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SmartSoftware;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;
using SmartSoftware.ClientSimulation.Demo.Scenarios;
using SmartSoftware.ClientSimulation.Scenarios;

namespace SmartSoftware.ClientSimulation.Demo;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcUiBasicThemeModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(ClientSimulationWebModule)
    )]
public class ClientSimulationDemoModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<ClientSimulationOptions>(options =>
        {
            options.Scenarios.Add(
                new ScenarioConfiguration(
                    typeof(DemoScenario),
                    clientCount: 20
                )
            );
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseConfiguredEndpoints();
    }
}
