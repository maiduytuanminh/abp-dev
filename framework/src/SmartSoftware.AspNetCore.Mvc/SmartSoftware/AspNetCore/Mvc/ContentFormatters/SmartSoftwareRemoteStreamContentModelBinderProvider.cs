using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartSoftware.Content;

namespace SmartSoftware.AspNetCore.Mvc.ContentFormatters;

public class SmartSoftwareRemoteStreamContentModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(RemoteStreamContent) ||
            typeof(IEnumerable<RemoteStreamContent>).IsAssignableFrom(context.Metadata.ModelType))
        {
            return new SmartSoftwareRemoteStreamContentModelBinder<RemoteStreamContent>();
        }

        if (context.Metadata.ModelType == typeof(IRemoteStreamContent) ||
            typeof(IEnumerable<IRemoteStreamContent>).IsAssignableFrom(context.Metadata.ModelType))
        {
            return new SmartSoftwareRemoteStreamContentModelBinder<IRemoteStreamContent>();
        }

        return null;
    }
}
