using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Autofac;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.Versioning;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreTestBaseModule),
    typeof(SmartSoftwareAspNetCoreMvcModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareHttpClientModule)
    )]
public class SmartSoftwareAspNetCoreMvcVersioningTestModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<SmartSoftwareAspNetCoreMvcOptions>(options =>
        {
                //2.0 Version
                options.ConventionalControllers.Create(typeof(SmartSoftwareAspNetCoreMvcVersioningTestModule).Assembly, opts =>
            {
                opts.TypePredicate = t => t.Namespace == typeof(SmartSoftware.AspNetCore.Mvc.Versioning.App.v2.TodoAppService).Namespace;
                opts.ApiVersions.Add(new ApiVersion(2, 0));
            });

                //1.0 Compatibility version
                options.ConventionalControllers.Create(typeof(SmartSoftwareAspNetCoreMvcVersioningTestModule).Assembly, opts =>
            {
                opts.TypePredicate = t => t.Namespace == typeof(SmartSoftware.AspNetCore.Mvc.Versioning.App.v1.TodoAppService).Namespace;
                opts.ApiVersions.Add(new ApiVersion(1, 0));
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var preActions = context.Services.GetPreConfigureActions<SmartSoftwareAspNetCoreMvcOptions>();
        Configure<SmartSoftwareAspNetCoreMvcOptions>(options =>
        {
            preActions.Configure(options);
        });

        context.Services.AddSmartSoftwareApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;

            //options.ApiVersionReader = new HeaderApiVersionReader("api-version"); //Supports header too
            //options.ApiVersionReader = new MediaTypeApiVersionReader(); //Supports accept header too
        }, options =>
        {
            options.ConfigureSmartSoftware(preActions.Configure());
        });

        context.Services.AddHttpClientProxies(typeof(SmartSoftwareAspNetCoreMvcVersioningTestModule).Assembly);

        Configure<SmartSoftwareRemoteServiceOptions>(options =>
        {
            options.RemoteServices.Default = new RemoteServiceConfiguration("/");
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        app.UseRouting();
        app.UseConfiguredEndpoints();
    }
}
