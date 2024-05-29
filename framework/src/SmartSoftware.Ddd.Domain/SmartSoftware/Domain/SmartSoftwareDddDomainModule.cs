using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Auditing;
using SmartSoftware.Caching;
using SmartSoftware.Data;
using SmartSoftware.Domain.ChangeTracking;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.EventBus;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.Guids;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectMapping;
using SmartSoftware.Specifications;
using SmartSoftware.Timing;

namespace SmartSoftware.Domain;

[DependsOn(
    typeof(SmartSoftwareAuditingModule),
    typeof(SmartSoftwareDataModule),
    typeof(SmartSoftwareEventBusModule),
    typeof(SmartSoftwareGuidsModule),
    typeof(SmartSoftwareTimingModule),
    typeof(SmartSoftwareObjectMappingModule),
    typeof(SmartSoftwareExceptionHandlingModule),
    typeof(SmartSoftwareSpecificationsModule),
    typeof(SmartSoftwareCachingModule),
    typeof(SmartSoftwareDddDomainSharedModule)
    )]
public class SmartSoftwareDddDomainModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new SmartSoftwareRepositoryConventionalRegistrar());
        context.Services.OnRegistered(ChangeTrackingInterceptorRegistrar.RegisterIfNeeded);
    }
}
