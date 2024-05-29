using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Json.SystemTextJson;
using SmartSoftware.Json.SystemTextJson.JsonConverters;

namespace SmartSoftware.AspNetCore.Mvc.Json;

public static class MvcCoreBuilderExtensions
{
    public static IMvcCoreBuilder AddSmartSoftwareJson(this IMvcCoreBuilder builder)
    {
        builder.Services.AddOptions<JsonOptions>()
            .Configure<IServiceProvider>((options, rootServiceProvider) =>
            {
                options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                options.JsonSerializerOptions.AllowTrailingCommas = true;

                options.JsonSerializerOptions.Converters.Add(new SmartSoftwareStringToEnumFactory());
                options.JsonSerializerOptions.Converters.Add(new SmartSoftwareStringToBooleanConverter());
                options.JsonSerializerOptions.Converters.Add(new SmartSoftwareStringToGuidConverter());
                options.JsonSerializerOptions.Converters.Add(new SmartSoftwareNullableStringToGuidConverter());
                options.JsonSerializerOptions.Converters.Add(new ObjectToInferredTypesConverter());

                options.JsonSerializerOptions.TypeInfoResolver = new SmartSoftwareDefaultJsonTypeInfoResolver(rootServiceProvider
                    .GetRequiredService<IOptions<SmartSoftwareSystemTextJsonSerializerModifiersOptions>>());
            });

        return builder;
    }
}
