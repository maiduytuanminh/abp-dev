using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Uow;

public class AlwaysDisableUnitOfWorkTransaction_Tests: SmartSoftwareIntegratedTest<SmartSoftwareUnitOfWorkModule>
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.AddAlwaysDisableUnitOfWorkTransaction();
    }

    [Fact]
    public void AlwaysDisableUnitOfWorkTransaction_Test()
    {
        GetService<UnitOfWorkManager>().ShouldNotBeNull();

        var unitOfWorkManager = ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        unitOfWorkManager.GetType().ShouldBe(typeof(AlwaysDisableTransactionsUnitOfWorkManager));

        using (var uow = unitOfWorkManager.Begin(isTransactional: true))
        {
            uow.Options.IsTransactional.ShouldBeFalse();
        }
    }
}
