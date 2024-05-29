using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Settings;

public class SettingValueProviderManager_Tests: SmartSoftwareIntegratedTest<SmartSoftwareSettingsTestModule>
{
    private readonly ISettingValueProviderManager _settingValueProviderManager;

    public SettingValueProviderManager_Tests()
    {
        _settingValueProviderManager = GetRequiredService<ISettingValueProviderManager>();
    }
    
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
        options.Services.Configure<SmartSoftwareSettingOptions>(settingOptions =>
        {
            settingOptions.ValueProviders.Add<TestDuplicateSettingValueProvider>();
        });
    }
    
    [Fact]
    public void Should_Throw_Exception_If_Duplicate_Provider_Name_Detected()
    {
        var exception = Assert.Throws<SmartSoftwareException>(() =>
        {
            var providers = _settingValueProviderManager.Providers;
        });
        
        exception.Message.ShouldBe($"Duplicate setting value provider name detected: {TestDuplicateSettingValueProvider.ProviderName}. Providers:{Environment.NewLine}{typeof(TestDuplicateSettingValueProvider).FullName}{Environment.NewLine}{typeof(TestSettingValueProvider).FullName}");
    }
}

public class TestDuplicateSettingValueProvider : ISettingValueProvider, ITransientDependency
{
    public const string ProviderName = "Test";


    public string Name => ProviderName;

    public TestDuplicateSettingValueProvider()
    {
    }

    public Task<string> GetOrNullAsync(SettingDefinition setting)
    {
        throw new NotImplementedException();
    }

    public Task<List<SettingValue>> GetAllAsync(SettingDefinition[] settings)
    {
        throw new NotImplementedException();
    }
}