using System;
using System.Linq;
using Asp.Versioning;
using Asp.Versioning.ApplicationModels;
using SmartSoftware.ApiVersioning;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.Conventions;
using SmartSoftware.AspNetCore.Mvc.Versioning;

namespace Microsoft.Extensions.DependencyInjection;

public static class SmartSoftwareApiVersioningExtensions
{
    public static IApiVersioningBuilder AddSmartSoftwareApiVersioning(
        this IServiceCollection services,
        Action<ApiVersioningOptions>? apiVersioningOptionsSetupAction = null,
        Action<MvcApiVersioningOptions>? mvcApiVersioningOptionsSetupAction = null)
    {
        services.AddTransient<IRequestedApiVersion, HttpContextRequestedApiVersion>();
        services.AddTransient<IApiControllerSpecification, SmartSoftwareConventionalApiControllerSpecification>();

        apiVersioningOptionsSetupAction ??= _ => { };
        mvcApiVersioningOptionsSetupAction ??= _ => { };
        return services.AddApiVersioning(apiVersioningOptionsSetupAction).AddMvc(mvcApiVersioningOptionsSetupAction);
    }

    public static void ConfigureSmartSoftware(this MvcApiVersioningOptions options, SmartSoftwareAspNetCoreMvcOptions mvcOptions)
    {
        foreach (var setting in mvcOptions.ConventionalControllers.ConventionalControllerSettings)
        {
            if (setting.MvcApiVersioningConfigurer == null)
            {
                ConfigureApiVersionsByConvention(options, setting);
            }
            else
            {
                setting.MvcApiVersioningConfigurer.Invoke(options);
            }
        }
    }

    private static void ConfigureApiVersionsByConvention(MvcApiVersioningOptions options, ConventionalControllerSetting setting)
    {
        foreach (var controllerType in setting.ControllerTypes)
        {
            var controllerBuilder = options.Conventions.Controller(controllerType);

            if (setting.ApiVersions.Any())
            {
                foreach (var apiVersion in setting.ApiVersions)
                {
                    controllerBuilder.HasApiVersion(apiVersion);
                }
            }
            else
            {
                if (!controllerType.IsDefined(typeof(ApiVersionAttribute), true))
                {
                    controllerBuilder.IsApiVersionNeutral();
                }
            }
        }
    }
}
