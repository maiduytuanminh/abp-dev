﻿using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.Modularity;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Data;

public class ConnectionStringResolver_Tests : SmartSoftwareIntegratedTest<ConnectionStringResolver_Tests.TestModule>
{
    private const string DefaultConnString = "default-value";
    private const string Database1Name = "Database1";
    private const string Database1ConnString = "database-1-value";
    private const string Database2Name = "Database2";

    private readonly IConnectionStringResolver _connectionStringResolver;

    public ConnectionStringResolver_Tests()
    {
        _connectionStringResolver = ServiceProvider.GetRequiredService<IConnectionStringResolver>();
    }

    [Fact]
    public async Task Should_Get_Default_ConnString_By_Default()
    {
        (await _connectionStringResolver.ResolveAsync()).ShouldBe(DefaultConnString);
    }

    [Fact]
    public async Task Should_Get_Specific_ConnString_IfDefined()
    {
        (await _connectionStringResolver.ResolveAsync(Database1Name)).ShouldBe(Database1ConnString);
    }

    [Fact]
    public async Task Should_Get_Default_ConnString_If_Not_Specified()
    {
        (await _connectionStringResolver.ResolveAsync(Database2Name)).ShouldBe(DefaultConnString);
    }

    [DependsOn(typeof(SmartSoftwareDataModule))]
    public class TestModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<SmartSoftwareDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = DefaultConnString;
                options.ConnectionStrings[Database1Name] = Database1ConnString;
            });
        }
    }
}
