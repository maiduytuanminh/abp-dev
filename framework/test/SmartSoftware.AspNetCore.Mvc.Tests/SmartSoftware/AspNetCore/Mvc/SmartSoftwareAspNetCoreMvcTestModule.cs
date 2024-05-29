using System;
using System.Collections.Generic;
using System.Security.Claims;
using Localization.Resources.SmartSoftwareUi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;
using SmartSoftware.AspNetCore.Mvc.GlobalFeatures;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.Localization.Resource;
using SmartSoftware.AspNetCore.Security.Claims;
using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Authorization;
using SmartSoftware.Autofac;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Localization;
using SmartSoftware.MemoryDb;
using SmartSoftware.Modularity;
using SmartSoftware.TestApp;
using SmartSoftware.TestApp.Application;
using SmartSoftware.Threading;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.AspNetCore.Mvc;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreTestBaseModule),
    typeof(SmartSoftwareMemoryDbTestModule),
    typeof(SmartSoftwareAspNetCoreMvcModule),
    typeof(SmartSoftwareAutofacModule)
    )]
public class SmartSoftwareAspNetCoreMvcTestModule : SmartSoftwareModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(MvcTestResource),
                typeof(SmartSoftwareAspNetCoreMvcTestModule).Assembly
            );
        });

        context.Services.PreConfigure<SmartSoftwareAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(TestAppModule).Assembly, opts =>
            {
                opts.UrlActionNameNormalizer = urlActionNameNormalizerContext =>
                    string.Equals(urlActionNameNormalizerContext.ActionNameInUrl, "phone", StringComparison.OrdinalIgnoreCase)
                        ? "phones"
                        : urlActionNameNormalizerContext.ActionNameInUrl;

                opts.TypePredicate = type => type != typeof(ConventionalAppService);
            });

            options.ExposeIntegrationServices = true;
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            GlobalFeatureManager.Instance.Modules.GetOrAdd(SmartSoftwareAspNetCoreMvcTestFeatures.ModuleName,
                () => new SmartSoftwareAspNetCoreMvcTestFeatures(GlobalFeatureManager.Instance))
                .EnableAll();
        });

        context.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = FakeAuthenticationSchemeDefaults.Scheme;
            options.DefaultChallengeScheme = "Bearer";
            options.DefaultForbidScheme = "Cookie";
        }).AddFakeAuthentication().AddCookie("Cookie").AddJwtBearer("Bearer", _ => { });

        context.Services.AddAuthorization(options =>
        {
            options.AddPolicy("MyClaimTestPolicy", policy =>
            {
                policy.RequireClaim("MyCustomClaimType", "42");
            });

            options.AddPolicy("TestPermission1_And_TestPermission2", policy =>
            {
                policy.Requirements.Add(new PermissionsRequirement(new []{"TestPermission1", "TestPermission2"}, requiresAll: true));
            });

            options.AddPolicy("TestPermission1_Or_TestPermission2", policy =>
            {
                policy.Requirements.Add(new PermissionsRequirement(new []{"TestPermission1", "TestPermission2"}, requiresAll: false));
            });
        });

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAspNetCoreMvcTestModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<MvcTestResource>("en")
                .AddBaseTypes(
                    typeof(SmartSoftwareUiResource),
                    typeof(SmartSoftwareValidationResource)
                ).AddVirtualJson("/SmartSoftware/AspNetCore/Mvc/Localization/Resource");

            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("el", "el", "Ελληνικά"));
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.RootDirectory = "/SmartSoftware/AspNetCore/Mvc";
        });

        Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationFormats.Add("/SmartSoftware/AspNetCore/App/Views/{1}/{0}.cshtml");
        });

        Configure<SmartSoftwareClaimsMapOptions>(options =>
        {
            options.Maps.Add("SerialNumber", () => ClaimTypes.SerialNumber);
            options.Maps.Add("DateOfBirth", () => ClaimTypes.DateOfBirth);
        });

        Configure<SmartSoftwareApplicationConfigurationOptions>(options =>
        {
            options.Contributors.Add(new TestApplicationConfigurationContributor());
        });

        context.Services.TransformSmartSoftwareClaims();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseSmartSoftwareRequestLocalization();
        app.UseSmartSoftwareSecurityHeaders();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints();
    }
}
