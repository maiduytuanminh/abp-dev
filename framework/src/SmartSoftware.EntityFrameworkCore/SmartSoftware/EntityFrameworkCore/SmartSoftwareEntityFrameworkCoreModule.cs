using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.Domain;
using SmartSoftware.EntityFrameworkCore.DistributedEvents;
using SmartSoftware.Modularity;
using SmartSoftware.Uow.EntityFrameworkCore;

namespace SmartSoftware.EntityFrameworkCore;

[DependsOn(typeof(SmartSoftwareDddDomainModule))]
public class SmartSoftwareEntityFrameworkCoreModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDbContextOptions>(options =>
        {
            options.PreConfigure(ssDbContextConfigurationContext =>
            {
                ssDbContextConfigurationContext.DbContextOptions
                    .ConfigureWarnings(warnings =>
                    {
                        warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning);
                    });
            });
        });

        context.Services.TryAddTransient(typeof(IDbContextProvider<>), typeof(UnitOfWorkDbContextProvider<>));
        context.Services.AddTransient(typeof(IDbContextEventOutbox<>), typeof(DbContextEventOutbox<>));
        context.Services.AddTransient(typeof(IDbContextEventInbox<>), typeof(DbContextEventInbox<>));
    }
}
