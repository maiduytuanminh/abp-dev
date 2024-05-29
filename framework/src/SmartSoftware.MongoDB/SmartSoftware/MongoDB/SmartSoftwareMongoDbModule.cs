using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.Domain;
using SmartSoftware.Domain.Repositories.MongoDB;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB.DependencyInjection;
using SmartSoftware.Uow.MongoDB;
using SmartSoftware.MongoDB.DistributedEvents;

namespace SmartSoftware.MongoDB;

[DependsOn(typeof(SmartSoftwareDddDomainModule))]
public class SmartSoftwareMongoDbModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new SmartSoftwareMongoDbConventionalRegistrar());
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddTransient(
            typeof(IMongoDbContextProvider<>),
            typeof(UnitOfWorkMongoDbContextProvider<>)
        );

        context.Services.TryAddTransient(
            typeof(IMongoDbRepositoryFilterer<>),
            typeof(MongoDbRepositoryFilterer<>)
        );

        context.Services.TryAddTransient(
            typeof(IMongoDbRepositoryFilterer<,>),
            typeof(MongoDbRepositoryFilterer<,>)
        );

        context.Services.AddTransient(
            typeof(IMongoDbContextEventOutbox<>),
            typeof(MongoDbContextEventOutbox<>)
        );

        context.Services.AddTransient(
            typeof(IMongoDbContextEventInbox<>),
            typeof(MongoDbContextEventInbox<>)
        );
    }
}
