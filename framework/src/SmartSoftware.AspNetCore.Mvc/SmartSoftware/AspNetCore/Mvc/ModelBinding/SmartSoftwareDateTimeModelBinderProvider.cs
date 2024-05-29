﻿using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartSoftware.Timing;

namespace SmartSoftware.AspNetCore.Mvc.ModelBinding;

public class SmartSoftwareDateTimeModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        var modelType = context.Metadata.UnderlyingOrModelType;
        if (modelType == typeof(DateTime))
        {
            if (context.Metadata.ContainerType == null)
            {
                if (context.Metadata is DefaultModelMetadata defaultModelMetadata &&
                    defaultModelMetadata.Attributes.Attributes.All(x => x.GetType() != typeof(DisableDateTimeNormalizationAttribute)))
                {
                    return CreateSmartSoftwareDateTimeModelBinder(context);
                }
            }
            else
            {
                var dateNormalizationDisabledForClass =
                    context.Metadata.ContainerType.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);

                var dateNormalizationDisabledForProperty = context.Metadata.ContainerType
                    .GetProperty(context.Metadata.PropertyName!)
                    ?.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);

                if (!dateNormalizationDisabledForClass &&
                    dateNormalizationDisabledForProperty != null &&
                    !dateNormalizationDisabledForProperty.Value)
                {
                    return CreateSmartSoftwareDateTimeModelBinder(context);
                }
            }
        }

        return null;
    }

    protected virtual SmartSoftwareDateTimeModelBinder CreateSmartSoftwareDateTimeModelBinder(ModelBinderProviderContext context)
    {
        const DateTimeStyles supportedStyles = DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal;
        var dateTimeModelBinder = new DateTimeModelBinder(supportedStyles, context.Services.GetRequiredService<ILoggerFactory>());
        return new SmartSoftwareDateTimeModelBinder(context.Services.GetRequiredService<IClock>(), dateTimeModelBinder);
    }
}
