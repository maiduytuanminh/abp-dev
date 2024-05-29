using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.MultiTenancy;
using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Json;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.AspNetCore.App;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMultiTenancyModule),
    typeof(SmartSoftwareAspNetCoreTestBaseModule)
    )]
public class AppModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareMultiTenancyOptions>(options =>
        {
            options.IsEnabled = true;
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseMultiTenancy();

        app.Run(async (ctx) =>
        {
            var currentTenant = ctx.RequestServices.GetRequiredService<ICurrentTenant>();
            var jsonSerializer = ctx.RequestServices.GetRequiredService<IJsonSerializer>();

            var dictionary = new Dictionary<string, string>
            {
                ["TenantId"] = currentTenant.IsAvailable ? currentTenant.Id.ToString() : ""
            };

            var result = jsonSerializer.Serialize(dictionary, camelCase: false);
            await ctx.Response.WriteAsync(result);
        });
    }
}
