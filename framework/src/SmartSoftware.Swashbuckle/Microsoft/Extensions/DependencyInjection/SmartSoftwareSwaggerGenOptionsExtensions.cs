using System;
using System.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;
using SmartSoftware.Swashbuckle;

namespace Microsoft.Extensions.DependencyInjection;

public static class SmartSoftwareSwaggerGenOptionsExtensions
{
    public static void HideSmartSoftwareEndpoints(this SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.DocumentFilter<SmartSoftwareSwashbuckleDocumentFilter>();
    }

    public static void UserFriendlyEnums(this SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.SchemaFilter<SmartSoftwareSwashbuckleEnumSchemaFilter>();
    }

    public static void CustomSmartSoftwareSchemaIds(this SwaggerGenOptions options)
    {
        string SchemaIdSelector(Type modelType)
        {
            if (!modelType.IsConstructedGenericType)
            {
                return modelType.FullName!.Replace("[]", "Array");
            }

            var prefix = modelType.GetGenericArguments()
                .Select(SchemaIdSelector)
                .Aggregate((previous, current) => previous + current);
            return modelType.FullName!.Split('`').First() + "Of" + prefix;
        }

        options.CustomSchemaIds(SchemaIdSelector);
    }
}
