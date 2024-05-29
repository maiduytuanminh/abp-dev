using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;
using Xunit;

namespace SmartSoftware.AspNetCore.Mvc;

public class SmartSoftwareAspNetCoreAsyncTestBase_Tests : SmartSoftwareAspNetCoreAsyncTestBase<SmartSoftwareAspNetCoreAsyncTestModule>
{
    [Fact]
    public async Task Get_API_Response_Test()
    {
        var result = await GetResponseAsStringAsync("/api");
        result.ShouldBe(await GetRequiredService<DataBaseService>().GetResponseAsync());
    }
}

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcModule),
    typeof(SmartSoftwareAspNetCoreTestBaseModule),
    typeof(SmartSoftwareAutofacModule)
)]
public class SmartSoftwareAspNetCoreAsyncTestModule : SmartSoftwareModule
{
    public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<DataBaseService>();
        return Task.CompletedTask;
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        var dataBaseService = app.ApplicationServices.GetRequiredService<DataBaseService>();

        var apiResponse = await dataBaseService.GetResponseAsync();
        app.Map("/api", _ =>
        {
            app.Run(async httpContext =>
            {
                await httpContext.Response.WriteAsync(apiResponse);
            });
        });

        app.UseRouting();
        app.UseConfiguredEndpoints();
    }
}

public class DataBaseService
{
    public Task<string> GetResponseAsync()
    {
        return Task.FromResult("hello api!");
    }
}
