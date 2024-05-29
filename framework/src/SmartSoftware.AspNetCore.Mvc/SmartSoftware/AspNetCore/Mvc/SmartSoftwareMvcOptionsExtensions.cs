using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.Auditing;
using SmartSoftware.AspNetCore.Mvc.ContentFormatters;
using SmartSoftware.AspNetCore.Mvc.Conventions;
using SmartSoftware.AspNetCore.Mvc.ExceptionHandling;
using SmartSoftware.AspNetCore.Mvc.Features;
using SmartSoftware.AspNetCore.Mvc.GlobalFeatures;
using SmartSoftware.AspNetCore.Mvc.ModelBinding;
using SmartSoftware.AspNetCore.Mvc.Response;
using SmartSoftware.AspNetCore.Mvc.Uow;
using SmartSoftware.AspNetCore.Mvc.Validation;
using SmartSoftware.Content;

namespace SmartSoftware.AspNetCore.Mvc;

internal static class SmartSoftwareMvcOptionsExtensions
{
    public static void AddSmartSoftware(this MvcOptions options, IServiceCollection services)
    {
        AddConventions(options, services);
        AddActionFilters(options);
        AddPageFilters(options);
        AddModelBinders(options);
        AddMetadataProviders(options, services);
        AddFormatters(options);
    }

    private static void AddFormatters(MvcOptions options)
    {
        options.OutputFormatters.Insert(0, new RemoteStreamContentOutputFormatter());
    }

    private static void AddConventions(MvcOptions options, IServiceCollection services)
    {
        options.Conventions.Add(new SmartSoftwareServiceConventionWrapper(services));
    }

    private static void AddActionFilters(MvcOptions options)
    {
        options.Filters.AddService(typeof(GlobalFeatureActionFilter));
        options.Filters.AddService(typeof(SmartSoftwareAuditActionFilter));
        options.Filters.AddService(typeof(SmartSoftwareNoContentActionFilter));
        options.Filters.AddService(typeof(SmartSoftwareFeatureActionFilter));
        options.Filters.AddService(typeof(SmartSoftwareValidationActionFilter));
        options.Filters.AddService(typeof(SmartSoftwareUowActionFilter));
        options.Filters.AddService(typeof(SmartSoftwareExceptionFilter));
    }

    private static void AddPageFilters(MvcOptions options)
    {
        options.Filters.AddService(typeof(GlobalFeaturePageFilter));
        options.Filters.AddService(typeof(SmartSoftwareExceptionPageFilter));
        options.Filters.AddService(typeof(SmartSoftwareAuditPageFilter));
        options.Filters.AddService(typeof(SmartSoftwareFeaturePageFilter));
        options.Filters.AddService(typeof(SmartSoftwareUowPageFilter));
    }

    private static void AddModelBinders(MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new SmartSoftwareDateTimeModelBinderProvider());
        options.ModelBinderProviders.Insert(1, new SmartSoftwareExtraPropertiesDictionaryModelBinderProvider());
        options.ModelBinderProviders.Insert(2, new SmartSoftwareRemoteStreamContentModelBinderProvider());
    }

    private static void AddMetadataProviders(MvcOptions options, IServiceCollection services)
    {
        options.ModelMetadataDetailsProviders.Add(new SmartSoftwareDataAnnotationAutoLocalizationMetadataDetailsProvider(services));

        options.ModelMetadataDetailsProviders.Add(new BindingSourceMetadataProvider(typeof(IRemoteStreamContent), BindingSource.FormFile));
        options.ModelMetadataDetailsProviders.Add(new BindingSourceMetadataProvider(typeof(IEnumerable<IRemoteStreamContent>), BindingSource.FormFile));
        options.ModelMetadataDetailsProviders.Add(new BindingSourceMetadataProvider(typeof(RemoteStreamContent), BindingSource.FormFile));
        options.ModelMetadataDetailsProviders.Add(new BindingSourceMetadataProvider(typeof(IEnumerable<RemoteStreamContent>), BindingSource.FormFile));
        options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(IRemoteStreamContent)));
        options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(RemoteStreamContent)));
    }
}
