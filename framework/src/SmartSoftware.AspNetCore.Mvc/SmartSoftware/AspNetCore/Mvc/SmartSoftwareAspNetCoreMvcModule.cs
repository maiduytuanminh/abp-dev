using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using SmartSoftware.ApiVersioning;
using SmartSoftware.Application;
using SmartSoftware.AspNetCore.Mvc.AntiForgery;
using SmartSoftware.AspNetCore.Mvc.ApiExploring;
using SmartSoftware.AspNetCore.Mvc.ApplicationModels;
using SmartSoftware.AspNetCore.Mvc.Conventions;
using SmartSoftware.AspNetCore.Mvc.DataAnnotations;
using SmartSoftware.AspNetCore.Mvc.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.Infrastructure;
using SmartSoftware.AspNetCore.Mvc.Json;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.VirtualFileSystem;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Http;
using SmartSoftware.DynamicProxy;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Http.Modeling;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Json;
using SmartSoftware.Json.SystemTextJson;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.UI;
using SmartSoftware.UI.Navigation;
using SmartSoftware.Validation.Localization;

namespace SmartSoftware.AspNetCore.Mvc;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreModule),
    typeof(SmartSoftwareLocalizationModule),
    typeof(SmartSoftwareApiVersioningAbstractionsModule),
    typeof(SmartSoftwareAspNetCoreMvcContractsModule),
    typeof(SmartSoftwareUiNavigationModule),
    typeof(SmartSoftwareGlobalFeaturesModule),
    typeof(SmartSoftwareDddApplicationModule),
    typeof(SmartSoftwareJsonSystemTextJsonModule)
    )]
public class SmartSoftwareAspNetCoreMvcModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        DynamicProxyIgnoreTypes.Add<ControllerBase>();
        DynamicProxyIgnoreTypes.Add<PageModel>();
        DynamicProxyIgnoreTypes.Add<ViewComponent>();

        context.Services.AddConventionalRegistrar(new SmartSoftwareAspNetCoreMvcConventionalRegistrar());
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareApiDescriptionModelOptions>(options =>
        {
            options.IgnoredInterfaces.AddIfNotContains(typeof(IAsyncActionFilter));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IFilterMetadata));
            options.IgnoredInterfaces.AddIfNotContains(typeof(IActionFilter));
        });

        Configure<SmartSoftwareRemoteServiceApiDescriptionProviderOptions>(options =>
        {
            var statusCodes = new List<int>
            {
                (int) HttpStatusCode.Forbidden,
                (int) HttpStatusCode.Unauthorized,
                (int) HttpStatusCode.BadRequest,
                (int) HttpStatusCode.NotFound,
                (int) HttpStatusCode.NotImplemented,
                (int) HttpStatusCode.InternalServerError
            };

            options.SupportedResponseTypes.AddIfNotContains(statusCodes.Select(statusCode => new ApiResponseType
            {
                Type = typeof(RemoteServiceErrorResponse),
                StatusCode = statusCode
            }));
        });

        context.Services.PostConfigure<SmartSoftwareAspNetCoreMvcOptions>(options =>
        {
            if (options.MinifyGeneratedScript == null)
            {
                options.MinifyGeneratedScript = context.Services.GetHostingEnvironment().IsProduction();
            }
        });

        var mvcCoreBuilder = context.Services.AddMvcCore(options =>
        {
            options.Filters.Add(new SmartSoftwareAutoValidateAntiforgeryTokenAttribute());
        });
        context.Services.ExecutePreConfiguredActions(mvcCoreBuilder);

        var ssMvcDataAnnotationsLocalizationOptions = context.Services
            .ExecutePreConfiguredActions(
                new SmartSoftwareMvcDataAnnotationsLocalizationOptions()
            );

        context.Services
            .AddSingleton<IOptions<SmartSoftwareMvcDataAnnotationsLocalizationOptions>>(
                new OptionsWrapper<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(
                    ssMvcDataAnnotationsLocalizationOptions
                )
            );

        var mvcBuilder = context.Services.AddMvc()
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var resourceType = ssMvcDataAnnotationsLocalizationOptions
                        .AssemblyResources
                        .GetOrDefault(type.Assembly);

                    if (resourceType != null)
                    {
                        return factory.Create(resourceType);
                    }

                    return factory.CreateDefaultOrNull() ??
                            factory.Create(type);
                };
            })
            .AddViewLocalization(); //TODO: How to configure from the application? Also, consider to move to a UI module since APIs does not care about it.

        if (context.Services.GetHostingEnvironment().IsDevelopment() &&
            context.Services.ExecutePreConfiguredActions<SmartSoftwareAspNetCoreMvcOptions>().EnableRazorRuntimeCompilationOnDevelopment)
        {
            mvcCoreBuilder.AddSmartSoftwareRazorRuntimeCompilation();
        }

        mvcCoreBuilder.AddSmartSoftwareJson();

        context.Services.ExecutePreConfiguredActions(mvcBuilder);

        //TODO: AddViewLocalization by default..?

        context.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

        //Use DI to create controllers
        mvcBuilder.AddControllersAsServices();

        //Use DI to create view components
        mvcBuilder.AddViewComponentsAsServices();

        //Use DI to create razor page
        context.Services.Replace(ServiceDescriptor.Singleton<IPageModelActivatorProvider, ServiceBasedPageModelActivatorProvider>());

        //Add feature providers
        var partManager = context.Services.GetSingletonInstance<ApplicationPartManager>();
        var application = context.Services.GetSingletonInstance<ISmartSoftwareApplication>();

        partManager.FeatureProviders.Add(new SmartSoftwareConventionalControllerFeatureProvider(application));
        partManager.ApplicationParts.AddIfNotContains(typeof(SmartSoftwareAspNetCoreMvcModule).Assembly);

        context.Services.Replace(ServiceDescriptor.Singleton<IValidationAttributeAdapterProvider, SmartSoftwareValidationAttributeAdapterProvider>());
        context.Services.AddSingleton<ValidationAttributeAdapterProvider>();

        context.Services.TryAddEnumerable(ServiceDescriptor.Transient<IActionDescriptorProvider, SmartSoftwareMvcActionDescriptorProvider>());
        context.Services.AddOptions<MvcOptions>()
            .Configure<IServiceProvider>((mvcOptions, serviceProvider) =>
            {
                mvcOptions.AddSmartSoftware(context.Services);

                // serviceProvider is root service provider.
                var stringLocalizer = serviceProvider.GetRequiredService<IStringLocalizer<SmartSoftwareValidationResource>>();
                mvcOptions.ModelBindingMessageProvider.SetValueIsInvalidAccessor(_ => stringLocalizer["The value '{0}' is invalid."]);
                mvcOptions.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => stringLocalizer["The field must be a number."]);
                mvcOptions.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(value => stringLocalizer["The field {0} must be a number.", value]);
            });

        Configure<SmartSoftwareEndpointRouterOptions>(options =>
        {
            options.EndpointConfigureActions.Add(endpointContext =>
            {
                endpointContext.Endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
                endpointContext.Endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpointContext.Endpoints.MapRazorPages();
            });
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule("ss");
        });

        context.Services.Replace(ServiceDescriptor.Singleton<IHttpResponseStreamWriterFactory, SmartSoftwareMemoryPoolHttpResponseStreamWriterFactory>());
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        ApplicationPartSorter.Sort(
            context.Services.GetSingletonInstance<ApplicationPartManager>(),
            context.Services.GetSingletonInstance<IModuleContainer>()
        );

        var preConfigureActions = context.Services.GetPreConfigureActions<SmartSoftwareAspNetCoreMvcOptions>();

        DynamicProxyIgnoreTypes.Add(preConfigureActions.Configure()
            .ConventionalControllers
            .ConventionalControllerSettings.SelectMany(x => x.ControllerTypes).ToArray());

        Configure<SmartSoftwareAspNetCoreMvcOptions>(options =>
        {
            preConfigureActions.Configure(options);
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AddApplicationParts(context);
    }

    private static void AddApplicationParts(ApplicationInitializationContext context)
    {
        var partManager = context.ServiceProvider.GetService<ApplicationPartManager>();
        if (partManager == null)
        {
            return;
        }

        var moduleContainer = context.ServiceProvider.GetRequiredService<IModuleContainer>();

        var plugInModuleAssemblies = moduleContainer
            .Modules
            .Where(m => m.IsLoadedAsPlugIn)
            .SelectMany(m => m.AllAssemblies)
            .Distinct();

        AddToApplicationParts(partManager, plugInModuleAssemblies);

        var controllerAssemblies = context
            .ServiceProvider
            .GetRequiredService<IOptions<SmartSoftwareAspNetCoreMvcOptions>>()
            .Value
            .ConventionalControllers
            .ConventionalControllerSettings
            .Select(s => s.Assembly)
            .Distinct();

        AddToApplicationParts(partManager, controllerAssemblies);

        var additionalAssemblies = moduleContainer
            .Modules
            .SelectMany(m => m.GetAdditionalAssemblies())
            .Distinct();

        AddToApplicationParts(partManager, additionalAssemblies);
    }

    private static void AddToApplicationParts(ApplicationPartManager partManager, IEnumerable<Assembly> moduleAssemblies)
    {
        foreach (var moduleAssembly in moduleAssemblies)
        {
            partManager.ApplicationParts.AddIfNotContains(moduleAssembly);
        }
    }
}
