﻿using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.Modularity;
using Xunit;

namespace SmartSoftware.Configuration;

public class ConfigurationAccessor_Tests
{
    [Fact]
    public async Task Should_Use_Empty_ConfigurationRoot_By_Default_Async()
    {
        using (var application = await SmartSoftwareApplicationFactory.CreateAsync<IndependentEmptyModule>())
        {
            var configuration1 = application.Services.GetConfiguration();
            configuration1.ShouldNotBeNull();

            await application.InitializeAsync();

            var configuration2 = ResolveConfiguration(application);

            configuration2.ShouldBe(configuration1);
        }
    }

    [Fact]
    public void Should_Use_Empty_ConfigurationRoot_By_Default()
    {
        using (var application = SmartSoftwareApplicationFactory.Create<IndependentEmptyModule>())
        {
            var configuration1 = application.Services.GetConfiguration();
            configuration1.ShouldNotBeNull();

            application.Initialize();

            var configuration2 = ResolveConfiguration(application);

            configuration2.ShouldBe(configuration1);
        }
    }

    [Fact]
    public async Task Should_Use_The_Registered_ConfigurationRoot_Async()
    {
        using (var application = await SmartSoftwareApplicationFactory.CreateAsync<IndependentEmptyModule>())
        {
            var myConfiguration = new ConfigurationBuilder().Build();
            application.Services.ReplaceConfiguration(myConfiguration);
            application.Services.GetConfiguration().ShouldBe(myConfiguration);

            await application.InitializeAsync();

            var configuration = ResolveConfiguration(application);

            configuration.ShouldBe(myConfiguration);
        }
    }

    [Fact]
    public void Should_Use_The_Registered_ConfigurationRoot()
    {
        using (var application = SmartSoftwareApplicationFactory.Create<IndependentEmptyModule>())
        {
            var myConfiguration = new ConfigurationBuilder().Build();
            application.Services.ReplaceConfiguration(myConfiguration);
            application.Services.GetConfiguration().ShouldBe(myConfiguration);

            application.Initialize();

            var configuration = ResolveConfiguration(application);

            configuration.ShouldBe(myConfiguration);
        }
    }

    private static IConfiguration ResolveConfiguration(ISmartSoftwareApplication application)
    {
        return application
            .ServiceProvider
            .GetRequiredService<IConfiguration>();
    }
}
