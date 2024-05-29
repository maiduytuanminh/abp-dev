﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.Security;
using SmartSoftware.Data;

namespace SmartSoftware.Settings;

[DependsOn(
    typeof(SmartSoftwareLocalizationAbstractionsModule),
    typeof(SmartSoftwareSecurityModule),
    typeof(SmartSoftwareDataModule)
)]
public class SmartSoftwareSettingsModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AutoAddDefinitionProviders(context.Services);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareSettingOptions>(options =>
        {
            options.ValueProviders.Add<DefaultValueSettingValueProvider>();
            options.ValueProviders.Add<ConfigurationSettingValueProvider>();
            options.ValueProviders.Add<GlobalSettingValueProvider>();
            options.ValueProviders.Add<UserSettingValueProvider>();
        });
    }

    private static void AutoAddDefinitionProviders(IServiceCollection services)
    {
        var definitionProviders = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(ISettingDefinitionProvider).IsAssignableFrom(context.ImplementationType))
            {
                definitionProviders.Add(context.ImplementationType);
            }
        });

        services.Configure<SmartSoftwareSettingOptions>(options =>
        {
            options.DefinitionProviders.AddIfNotContains(definitionProviders);
        });
    }
}
