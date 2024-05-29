using System;
using Microsoft.AspNetCore.Builder;
using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.SignalR;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreSignalRModule),
    typeof(SmartSoftwareAspNetCoreTestBaseModule),
    typeof(SmartSoftwareAutofacModule)
    )]
public class SmartSoftwareAspNetCoreSignalRTestModule : SmartSoftwareModule
{
    public static Exception UseConfiguredEndpointsException { get; set; }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseRouting();

        UseConfiguredEndpointsException = null;
        try
        {
            app.UseConfiguredEndpoints();
        }
        catch (Exception e)
        {
            UseConfiguredEndpointsException = e;
        }
    }
}
