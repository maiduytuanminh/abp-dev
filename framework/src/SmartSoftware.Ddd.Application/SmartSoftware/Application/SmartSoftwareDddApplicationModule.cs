using System.Collections.Generic;
using SmartSoftware.Application.Services;
using SmartSoftware.Aspects;
using SmartSoftware.Auditing;
using SmartSoftware.Authorization;
using SmartSoftware.Domain;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Http;
using SmartSoftware.Http.Modeling;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectMapping;
using SmartSoftware.Security;
using SmartSoftware.Settings;
using SmartSoftware.Uow;
using SmartSoftware.Validation;

namespace SmartSoftware.Application;

[DependsOn(
    typeof(SmartSoftwareDddDomainModule),
    typeof(SmartSoftwareDddApplicationContractsModule),
    typeof(SmartSoftwareSecurityModule),
    typeof(SmartSoftwareObjectMappingModule),
    typeof(SmartSoftwareValidationModule),
    typeof(SmartSoftwareAuthorizationModule),
    typeof(SmartSoftwareHttpAbstractionsModule),
    typeof(SmartSoftwareSettingsModule),
    typeof(SmartSoftwareFeaturesModule),
    typeof(SmartSoftwareGlobalFeaturesModule)
    )]
public class SmartSoftwareDddApplicationModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareApiDescriptionModelOptions>(options =>
        {
            options.IgnoredInterfaces.AddIfNotContains(typeof(IRemoteService));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IApplicationService));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IUnitOfWorkEnabled));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IAuditingEnabled));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IValidationEnabled));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IGlobalFeatureCheckingEnabled));
        });
    }
}
