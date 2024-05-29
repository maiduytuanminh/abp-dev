using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.Domain;
using SmartSoftware.Domain.Repositories.MemoryDb;
using SmartSoftware.Modularity;
using SmartSoftware.Uow.MemoryDb;

namespace SmartSoftware.MemoryDb;

[DependsOn(typeof(SmartSoftwareDddDomainModule))]
public class SmartSoftwareMemoryDbModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddTransient(typeof(IMemoryDatabaseProvider<>), typeof(UnitOfWorkMemoryDatabaseProvider<>));
        context.Services.TryAddTransient(typeof(IMemoryDatabaseCollection<>), typeof(MemoryDatabaseCollection<>));
    }
}
