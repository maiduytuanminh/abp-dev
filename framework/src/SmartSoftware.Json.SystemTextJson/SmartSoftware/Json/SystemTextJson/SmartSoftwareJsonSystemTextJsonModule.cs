using System;
using System.Text.Encodings.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Data;
using SmartSoftware.Json.SystemTextJson.JsonConverters;
using SmartSoftware.Json.SystemTextJson.Modifiers;
using SmartSoftware.Modularity;
using SmartSoftware.Timing;

namespace SmartSoftware.Json.SystemTextJson;

[DependsOn(typeof(SmartSoftwareJsonAbstractionsModule), typeof(SmartSoftwareTimingModule), typeof(SmartSoftwareDataModule))]
public class SmartSoftwareJsonSystemTextJsonModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddOptions<SmartSoftwareSystemTextJsonSerializerOptions>()
            .Configure<IServiceProvider>((options, rootServiceProvider) =>
            {
                // If the user hasn't explicitly configured the encoder, use the less strict encoder that does not encode all non-ASCII characters.
                options.JsonSerializerOptions.Encoder ??= JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

                options.JsonSerializerOptions.Converters.Add(new SmartSoftwareStringToEnumFactory());
                options.JsonSerializerOptions.Converters.Add(new SmartSoftwareStringToBooleanConverter());
                options.JsonSerializerOptions.Converters.Add(new SmartSoftwareStringToGuidConverter());
                options.JsonSerializerOptions.Converters.Add(new SmartSoftwareNullableStringToGuidConverter());
                options.JsonSerializerOptions.Converters.Add(new ObjectToInferredTypesConverter());

                options.JsonSerializerOptions.TypeInfoResolver = new SmartSoftwareDefaultJsonTypeInfoResolver(rootServiceProvider
                    .GetRequiredService<IOptions<SmartSoftwareSystemTextJsonSerializerModifiersOptions>>());
            });

        context.Services.AddOptions<SmartSoftwareSystemTextJsonSerializerModifiersOptions>()
            .Configure<IServiceProvider>((options, rootServiceProvider) =>
            {
                options.Modifiers.Add(new SmartSoftwareDateTimeConverterModifier().CreateModifyAction(rootServiceProvider));
            });
    }
}
