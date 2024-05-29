using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shouldly;
using Xunit;

namespace SmartSoftware.AspNetCore;

public class SmartSoftwareHostEnvironment_Tests : SmartSoftwareAspNetCoreTestBase<Program>
{
    [Fact]
    public void Should_Set_Environment_From_IWebHostEnvironment()
    {
        var ssHostEnvironment = GetRequiredService<ISmartSoftwareHostEnvironment>();
        ssHostEnvironment.EnvironmentName.ShouldBe(Environments.Staging);
    }
}

public class SmartSoftwareHostEnvironment_Async_Initialize_Tests
{
    [Fact]
    public async Task Should_Set_Environment_From_AspNetCore()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = Environments.Staging
        });
        builder.Host.UseAutofac();
        await builder.AddApplicationAsync<SmartSoftwareAspNetCoreTestModule>();
        var app = builder.Build();
        await app.InitializeApplicationAsync();

        var ssHostEnvironment = app.Services.GetRequiredService<ISmartSoftwareHostEnvironment>();
        ssHostEnvironment.EnvironmentName.ShouldBe(Environments.Staging);

        builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = Environments.Staging
        });
        builder.Host.UseAutofac();
        var ssApp = await SmartSoftwareApplicationFactory.CreateAsync<SmartSoftwareAspNetCoreTestModule>(builder.Services);
        app = builder.Build();
        await app.InitializeApplicationAsync();

        ssHostEnvironment = ssApp.Services.GetRequiredService<ISmartSoftwareHostEnvironment>();
        ssHostEnvironment.EnvironmentName.ShouldBe(Environments.Staging);
    }
}
