using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Validation;

[DependsOn(
    typeof(SmartSoftwareValidationAbstractionsModule),
    typeof(SmartSoftwareLocalizationModule)
    )]
public class SmartSoftwareValidationModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(ValidationInterceptorRegistrar.RegisterIfNeeded);
        AutoAddObjectValidationContributors(context.Services);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareValidationResource>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwareValidationResource>("en")
                .AddVirtualJson("/SmartSoftware/Validation/Localization");
        });
    }

    private static void AutoAddObjectValidationContributors(IServiceCollection services)
    {
        var contributorTypes = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(IObjectValidationContributor).IsAssignableFrom(context.ImplementationType))
            {
                contributorTypes.Add(context.ImplementationType);
            }
        });

        services.Configure<SmartSoftwareValidationOptions>(options =>
        {
            options.ObjectValidationContributors.AddIfNotContains(contributorTypes);
        });
    }
}
