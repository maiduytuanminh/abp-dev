using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using SmartSoftware.Authorization;
using SmartSoftware.Features.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Validation;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Features;

[DependsOn(
    typeof(SmartSoftwareLocalizationModule),
    typeof(SmartSoftwareMultiTenancyModule),
    typeof(SmartSoftwareValidationModule),
    typeof(SmartSoftwareAuthorizationAbstractionsModule)
    )]
public class SmartSoftwareFeaturesModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(FeatureInterceptorRegistrar.RegisterIfNeeded);
        AutoAddDefinitionProviders(context.Services);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Configure<SmartSoftwareFeatureOptions>(options =>
        {
            options.ValueProviders.Add<DefaultValueFeatureValueProvider>();
            options.ValueProviders.Add<EditionFeatureValueProvider>();
            options.ValueProviders.Add<TenantFeatureValueProvider>();
        });

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareFeatureResource>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwareFeatureResource>("en")
                .AddVirtualJson("/SmartSoftware/Features/Localization");
        });

        Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("SmartSoftware.Feature", typeof(SmartSoftwareFeatureResource));
        });
    }

    private static void AutoAddDefinitionProviders(IServiceCollection services)
    {
        var definitionProviders = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(IFeatureDefinitionProvider).IsAssignableFrom(context.ImplementationType))
            {
                definitionProviders.Add(context.ImplementationType);
            }
        });

        services.Configure<SmartSoftwareFeatureOptions>(options =>
        {
            options.DefinitionProviders.AddIfNotContains(definitionProviders);
        });
    }
}
