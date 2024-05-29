using System.Collections.Generic;
using Localization.Resources.SmartSoftwareUi;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.FeatureManagement.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.FeatureManagement.JsonConverters;

namespace SmartSoftware.FeatureManagement;

[DependsOn(
    typeof(SmartSoftwareFeatureManagementApplicationContractsModule),
    typeof(SmartSoftwareAspNetCoreMvcModule))]
public class SmartSoftwareFeatureManagementHttpApiModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareFeatureManagementHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SmartSoftwareFeatureManagementResource>()
                .AddBaseTypes(typeof(SmartSoftwareUiResource));
        });

        var valueValidatorFactoryOptions = context.Services.ExecutePreConfiguredActions<ValueValidatorFactoryOptions>();
        Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.AddIfNotContains(new StringValueTypeJsonConverter(valueValidatorFactoryOptions));
        });
    }
}
