using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using MyCompanyName.MyProjectName.Data;
using MyCompanyName.MyProjectName.Localization;
using OpenIddict.Validation.AspNetCore;
using SmartSoftware;
using SmartSoftware.Account;
using SmartSoftware.Account.Web;
using SmartSoftware.AspNetCore.MultiTenancy;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AspNetCore.Serilog;
using SmartSoftware.AuditLogging.MongoDB;
using SmartSoftware.Autofac;
using SmartSoftware.AutoMapper;
using SmartSoftware.Emailing;
using SmartSoftware.FeatureManagement;
using SmartSoftware.FeatureManagement.MongoDB;
using SmartSoftware.Identity;
using SmartSoftware.Identity.MongoDB;
using SmartSoftware.Localization;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.OpenIddict.MongoDB;
using SmartSoftware.PermissionManagement;
using SmartSoftware.PermissionManagement.MongoDB;
using SmartSoftware.PermissionManagement.HttpApi;
using SmartSoftware.PermissionManagement.Identity;
using SmartSoftware.PermissionManagement.OpenIddict;
using SmartSoftware.SettingManagement;
using SmartSoftware.SettingManagement.MongoDB;
using SmartSoftware.Swashbuckle;
using SmartSoftware.TenantManagement;
using SmartSoftware.TenantManagement.MongoDB;
using SmartSoftware.OpenIddict;
using SmartSoftware.Security.Claims;
using SmartSoftware.UI.Navigation.Urls;
using SmartSoftware.Uow;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    // SS Framework packages
    typeof(SmartSoftwareAspNetCoreMvcModule),
    typeof(SmartSoftwareAspNetCoreMultiTenancyModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareAutoMapperModule),
    typeof(SmartSoftwareAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(SmartSoftwareSwashbuckleModule),
    typeof(SmartSoftwareAspNetCoreSerilogModule),

    // Account module packages
    typeof(SmartSoftwareAccountApplicationModule),
    typeof(SmartSoftwareAccountHttpApiModule),
    typeof(SmartSoftwareAccountWebOpenIddictModule),

    // Identity module packages
    typeof(SmartSoftwarePermissionManagementDomainIdentityModule),
    typeof(SmartSoftwarePermissionManagementDomainOpenIddictModule),
    typeof(SmartSoftwareIdentityApplicationModule),
    typeof(SmartSoftwareIdentityHttpApiModule),
    typeof(SmartSoftwareIdentityMongoDbModule),
    typeof(SmartSoftwareOpenIddictMongoDbModule),

    // Audit logging module packages
    typeof(SmartSoftwareAuditLoggingMongoDbModule),

    // Permission Management module packages
    typeof(SmartSoftwarePermissionManagementApplicationModule),
    typeof(SmartSoftwarePermissionManagementHttpApiModule),
    typeof(SmartSoftwarePermissionManagementMongoDbModule),

    // Tenant Management module packages
    typeof(SmartSoftwareTenantManagementApplicationModule),
    typeof(SmartSoftwareTenantManagementHttpApiModule),
    typeof(SmartSoftwareTenantManagementMongoDbModule),

    // Feature Management module packages
    typeof(SmartSoftwareFeatureManagementApplicationModule),
    typeof(SmartSoftwareFeatureManagementMongoDbModule),
    typeof(SmartSoftwareFeatureManagementHttpApiModule),

    // Setting Management module packages
    typeof(SmartSoftwareSettingManagementApplicationModule),
    typeof(SmartSoftwareSettingManagementMongoDbModule),
    typeof(SmartSoftwareSettingManagementHttpApiModule)
)]
public class MyProjectNameModule : SmartSoftwareModule
{
    /* Single point to enable/disable multi-tenancy */
    private const bool IsMultiTenant = true;

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        context.Services.PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(MyProjectNameResource)
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("MyProjectName");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });

        if (!hostingEnvironment.IsDevelopment())
        {
            PreConfigure<SmartSoftwareOpenIddictAspNetCoreOptions>(options =>
            {
                options.AddDevelopmentEncryptionAndSigningCertificate = false;
            });

            PreConfigure<OpenIddictServerBuilder>(serverBuilder =>
            {
                serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx", "00000000-0000-0000-0000-000000000000");
            });
        }
        
        MyProjectNameGlobalFeatureConfigurator.Configure();
        MyProjectNameModuleExtensionConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        if (hostingEnvironment.IsDevelopment())
        {
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
        }

        ConfigureAuthentication(context);
        ConfigureBundles();
        ConfigureMultiTenancy();
        ConfigureUrls(configuration);
        ConfigureAutoMapper(context);
        ConfigureSwagger(context.Services, configuration);
        ConfigureAutoApiControllers();
        ConfigureVirtualFiles(hostingEnvironment);
        ConfigureLocalization();
        ConfigureCors(context, configuration);
        ConfigureDataProtection(context);
        ConfigureMongoDB(context);
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        context.Services.Configure<SmartSoftwareClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });
    }

    private void ConfigureBundles()
    {
        Configure<SmartSoftwareBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle => { bundle.AddFiles("/global-styles.css"); }
            );
        });
    }

    private void ConfigureMultiTenancy()
    {
        Configure<SmartSoftwareMultiTenancyOptions>(options =>
        {
            options.IsEnabled = IsMultiTenant;
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ?? Array.Empty<string>());

            options.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
            options.Applications["Angular"].Urls[AccountUrlNames.PasswordReset] = "account/reset-password";
        });
    }

    private void ConfigureLocalization()
    {
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<MyProjectNameResource>("en")
                .AddBaseTypes(typeof(SmartSoftwareValidationResource))
                .AddVirtualJson("/Localization/MyProjectName");

            options.DefaultResourceType = typeof(MyProjectNameResource);

            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi"));
            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch"));
            options.Languages.Add(new LanguageInfo("es", "es", "Español"));
            options.Languages.Add(new LanguageInfo("el", "el", "Ελληνικά"));
        });

        Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("MyProjectName", typeof(MyProjectNameResource));
        });
    }

    private void ConfigureVirtualFiles(IWebHostEnvironment hostingEnvironment)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MyProjectNameModule>();
            if (hostingEnvironment.IsDevelopment())
            {
                /* Using physical files in development, so we don't need to recompile on changes */
                options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameModule>(hostingEnvironment.ContentRootPath);
            }
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<SmartSoftwareAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(MyProjectNameModule).Assembly);
        });
    }

    private void ConfigureSwagger(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSmartSoftwareSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"]!,
            new Dictionary<string, string>
            {
                    {"MyProjectName", "MyProjectName API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyProjectName API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
    }

    private void ConfigureAutoMapper(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<MyProjectNameModule>();
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            /* Uncomment `validate: true` if you want to enable the Configuration Validation feature.
             * See AutoMapper's documentation to learn what it is:
             * https://docs.automapper.org/en/stable/Configuration-validation.html
             */
            options.AddMaps<MyProjectNameModule>(/* validate: true */);
        });
    }

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]?
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray() ?? Array.Empty<string>()
                    )
                    .WithSmartSoftwareExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    private void ConfigureDataProtection(ServiceConfigurationContext context)
    {
        context.Services.AddDataProtection().SetApplicationName("MyProjectName");
    }

    private void ConfigureMongoDB(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<MyProjectNameDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });

        Configure<SmartSoftwareUnitOfWorkDefaultOptions>(options =>
        {
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSmartSoftwareRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseSmartSoftwareOpenIddictValidation();

        if (IsMultiTenant)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSmartSoftwareSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyProjectName API");
        });

        app.UseAuditing();
        app.UseSmartSoftwareSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
