using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.FeatureManagement.JsonConverters;
using SmartSoftware.FeatureManagement.Localization;
using SmartSoftware.Json.SystemTextJson;
using SmartSoftware.Localization;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Modularity;
using SmartSoftware.Validation;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.FeatureManagement;

[DependsOn(
    typeof(SmartSoftwareValidationModule),
    typeof(SmartSoftwareJsonSystemTextJsonModule)
)]
public class SmartSoftwareFeatureManagementDomainSharedModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareFeatureManagementDomainSharedModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwareFeatureManagementResource>("en")
                .AddBaseTypes(
                    typeof(SmartSoftwareValidationResource)
                ).AddVirtualJson("SmartSoftware/FeatureManagement/Localization/Domain");
        });

        Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("SmartSoftware.FeatureManagement", typeof(SmartSoftwareFeatureManagementResource));
        });

        var valueValidatorFactoryOptions = context.Services.GetPreConfigureActions<ValueValidatorFactoryOptions>();
        Configure<ValueValidatorFactoryOptions>(options =>
        {
            valueValidatorFactoryOptions.Configure(options);
        });

        Configure<SmartSoftwareSystemTextJsonSerializerOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new StringValueTypeJsonConverter(valueValidatorFactoryOptions.Configure()));
        });
    }
}
