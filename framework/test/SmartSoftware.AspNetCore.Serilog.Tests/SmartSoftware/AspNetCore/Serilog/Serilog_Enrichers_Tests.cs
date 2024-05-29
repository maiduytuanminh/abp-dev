using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog.Events;
using Shouldly;
using SmartSoftware.AspNetCore.App;
using SmartSoftware.AspNetCore.MultiTenancy;
using SmartSoftware.MultiTenancy;
using SmartSoftware.MultiTenancy.ConfigurationStore;
using Xunit;

namespace SmartSoftware.AspNetCore.Serilog;

public class Serilog_Enrichers_Tests : SmartSoftwareSerilogTestBase
{
    private const string ExecutedEndpointLogEventText = "Executed endpoint '{EndpointName}'";

    private readonly Guid _testTenantId = Guid.NewGuid();
    private readonly string _testTenantName = "acme";
    private readonly string _testTenantNormalizedName = "ACME";

    private readonly SmartSoftwareAspNetCoreMultiTenancyOptions _tenancyOptions;
    private readonly SmartSoftwareAspNetCoreSerilogOptions _serilogOptions;
    private readonly ILogger<Serilog_Enrichers_Tests> _logger;

    public Serilog_Enrichers_Tests()
    {
        _tenancyOptions = ServiceProvider.GetRequiredService<IOptions<SmartSoftwareAspNetCoreMultiTenancyOptions>>().Value;
        _serilogOptions =
            ServiceProvider.GetRequiredService<IOptions<SmartSoftwareAspNetCoreSerilogOptions>>().Value;
        _logger = ServiceProvider.GetRequiredService<ILogger<Serilog_Enrichers_Tests>>();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<SmartSoftwareDefaultTenantStoreOptions>(options =>
        {
            options.Tenants = new[]
            {
                new TenantConfiguration(_testTenantId, _testTenantName, _testTenantNormalizedName)
            };
        });
        base.ConfigureServices(services);
    }

    [Fact]
    public async Task TenantId_Not_Set_Test()
    {
        var url = GetUrl<SerilogTestController>(nameof(SerilogTestController.Index));
        var result = await GetResponseAsStringAsync(url);

        var executedLogEvent = GetLogEvent(ExecutedEndpointLogEventText);

        executedLogEvent.ShouldNotBeNull();
        executedLogEvent.Properties.ContainsKey(_serilogOptions.EnricherPropertyNames.TenantId)
            .ShouldBe(false);
    }

    [Fact]
    public async Task TenantId_Set_Test()
    {
        var url =
            GetUrl<SerilogTestController>(nameof(SerilogTestController.Index)) +
            $"?{_tenancyOptions.TenantKey}={_testTenantName}";
        var result = await GetResponseAsStringAsync(url);

        var executedLogEvent = GetLogEvent(ExecutedEndpointLogEventText);

        executedLogEvent.ShouldNotBeNull();
        executedLogEvent.Properties.ContainsKey(_serilogOptions.EnricherPropertyNames.TenantId)
            .ShouldBe(true);
        ((ScalarValue)executedLogEvent.Properties[_serilogOptions.EnricherPropertyNames.TenantId]).Value
            .ShouldBe(_testTenantId);
    }

    [Fact]
    public async Task CorrelationId_Enrichers_Test()
    {
        var url = GetUrl<SerilogTestController>(nameof(SerilogTestController.CorrelationId));
        var result = await GetResponseAsStringAsync(url);

        var executedLogEvent = GetLogEvent(ExecutedEndpointLogEventText);

        executedLogEvent.ShouldNotBeNull();

        executedLogEvent.Properties.ContainsKey(_serilogOptions.EnricherPropertyNames.CorrelationId)
            .ShouldBeTrue();

        ((ScalarValue)executedLogEvent.Properties[_serilogOptions.EnricherPropertyNames.CorrelationId]).Value
            .ShouldBe(result);
    }
}
