using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using SmartSoftware.Modularity;
using SmartSoftware.Testing;
using SmartSoftware.Uow;
using Xunit;

namespace SmartSoftware.Data;

public class DataSeederExtensions_Tests : SmartSoftwareIntegratedTest<DataSeederExtensions_Tests.TestModule>
{
    private IDataSeeder _dataSeeder;

    protected override void AfterAddApplication(IServiceCollection services)
    {
        _dataSeeder = Substitute.For<IDataSeeder>();
        services.Replace(ServiceDescriptor.Singleton(_dataSeeder));
        base.AfterAddApplication(services);
    }

    [Fact]
    public void SeedInSeparateUowAsync()
    {
        var tenantId = Guid.NewGuid();
        _dataSeeder.SeedInSeparateUowAsync(tenantId, new SmartSoftwareUnitOfWorkOptions(true, IsolationLevel.Serializable, 888), true);

        _dataSeeder.Received().SeedAsync(Arg.Is<DataSeedContext>(x => x.TenantId == tenantId &&
                                                                      x.Properties[DataSeederExtensions.SeedInSeparateUow].To<bool>() == true &&
                                                                      x.Properties[DataSeederExtensions.SeedInSeparateUowOptions].As<SmartSoftwareUnitOfWorkOptions>().IsTransactional == true &&
                                                                      x.Properties[DataSeederExtensions.SeedInSeparateUowOptions].As<SmartSoftwareUnitOfWorkOptions>().IsolationLevel == IsolationLevel.Serializable &&
                                                                      x.Properties[DataSeederExtensions.SeedInSeparateUowOptions].As<SmartSoftwareUnitOfWorkOptions>().Timeout == 888 &&
                                                                      x.Properties[DataSeederExtensions.SeedInSeparateUowRequiresNew].To<bool>() == true));
    }

    [DependsOn(typeof(SmartSoftwareDataModule))]
    public class TestModule : SmartSoftwareModule
    {

    }
}
