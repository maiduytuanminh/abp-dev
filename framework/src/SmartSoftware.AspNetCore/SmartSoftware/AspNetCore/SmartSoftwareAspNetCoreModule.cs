using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using SmartSoftware.AspNetCore.Auditing;
using SmartSoftware.AspNetCore.VirtualFileSystem;
using SmartSoftware.Auditing;
using SmartSoftware.Authorization;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.Http;
using SmartSoftware.Modularity;
using SmartSoftware.Security;
using SmartSoftware.Uow;
using SmartSoftware.Validation;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.AspNetCore;

[DependsOn(
    typeof(SmartSoftwareAuditingModule),
    typeof(SmartSoftwareSecurityModule),
    typeof(SmartSoftwareVirtualFileSystemModule),
    typeof(SmartSoftwareUnitOfWorkModule),
    typeof(SmartSoftwareHttpModule),
    typeof(SmartSoftwareAuthorizationModule),
    typeof(SmartSoftwareValidationModule),
    typeof(SmartSoftwareExceptionHandlingModule),
    typeof(SmartSoftwareAspNetCoreAbstractionsModule)
    )]
public class SmartSoftwareAspNetCoreModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var ssHostEnvironment = context.Services.GetSingletonInstance<ISmartSoftwareHostEnvironment>();
        if (ssHostEnvironment.EnvironmentName.IsNullOrWhiteSpace())
        {
            ssHostEnvironment.EnvironmentName = context.Services.GetHostingEnvironment().EnvironmentName;
        }
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAuthorization();

        Configure<SmartSoftwareAuditingOptions>(options =>
        {
            options.Contributors.Add(new AspNetCoreAuditLogContributor());
        });

        Configure<StaticFileOptions>(options =>
        {
            options.ContentTypeProvider = context.Services.GetRequiredService<SmartSoftwareFileExtensionContentTypeProvider>();
        });

        AddAspNetServices(context.Services);
        context.Services.AddObjectAccessor<IApplicationBuilder>();
        context.Services.AddSmartSoftwareDynamicOptions<RequestLocalizationOptions, SmartSoftwareRequestLocalizationOptionsManager>();
    }

    private static void AddAspNetServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var environment = context.GetEnvironmentOrNull();
        if (environment != null)
        {
            environment.WebRootFileProvider =
                new CompositeFileProvider(
                    context.GetEnvironment().WebRootFileProvider,
                    context.ServiceProvider.GetRequiredService<IWebContentFileProvider>()
                );
        }
    }
}
