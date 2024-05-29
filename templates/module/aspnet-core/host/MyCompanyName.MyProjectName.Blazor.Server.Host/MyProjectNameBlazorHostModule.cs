using System;
using System.IO;
using System.Threading.Tasks;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MyCompanyName.MyProjectName.Blazor.Server.Host.Components;
using MyCompanyName.MyProjectName.Blazor.Server.Host.Menus;
using MyCompanyName.MyProjectName.EntityFrameworkCore;
using MyCompanyName.MyProjectName.Localization;
using MyCompanyName.MyProjectName.MultiTenancy;
using OpenIddict.Validation.AspNetCore;
using SmartSoftware;
using SmartSoftware.Account;
using SmartSoftware.Account.Web;
using SmartSoftware.AspNetCore.Components.Server;
using SmartSoftware.AspNetCore.Components.Server.BasicTheme;
using SmartSoftware.AspNetCore.Components.Server.BasicTheme.Bundling;
using SmartSoftware.AspNetCore.Components.Web.Theming.Routing;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using SmartSoftware.AspNetCore.Serilog;
using SmartSoftware.AuditLogging.EntityFrameworkCore;
using SmartSoftware.Autofac;
using SmartSoftware.Data;
using SmartSoftware.Emailing;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.FeatureManagement;
using SmartSoftware.FeatureManagement.EntityFrameworkCore;
using SmartSoftware.Identity;
using SmartSoftware.Identity.Blazor.Server;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.PermissionManagement;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.PermissionManagement.Identity;
using SmartSoftware.SettingManagement;
using SmartSoftware.SettingManagement.Blazor.Server;
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.Swashbuckle;
using SmartSoftware.TenantManagement;
using SmartSoftware.TenantManagement.Blazor.Server;
using SmartSoftware.TenantManagement.EntityFrameworkCore;
using SmartSoftware.UI.Navigation;
using SmartSoftware.UI.Navigation.Urls;
using SmartSoftware.VirtualFileSystem;

namespace MyCompanyName.MyProjectName.Blazor.Server.Host;

[DependsOn(
    typeof(MyProjectNameEntityFrameworkCoreModule),
    typeof(MyProjectNameApplicationModule),
    typeof(MyProjectNameHttpApiModule),
    typeof(SmartSoftwareAspNetCoreMvcUiBasicThemeModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareSwashbuckleModule),
    typeof(SmartSoftwareAspNetCoreSerilogModule),
    typeof(SmartSoftwareAccountWebOpenIddictModule),
    typeof(SmartSoftwareAccountApplicationModule),
    typeof(SmartSoftwareAccountHttpApiModule),
    typeof(SmartSoftwareAspNetCoreComponentsServerBasicThemeModule),
    typeof(SmartSoftwareIdentityApplicationModule),
    typeof(SmartSoftwareIdentityEntityFrameworkCoreModule),
    typeof(SmartSoftwareAuditLoggingEntityFrameworkCoreModule),
    typeof(SmartSoftwareIdentityBlazorServerModule),
    typeof(SmartSoftwareFeatureManagementApplicationModule),
    typeof(SmartSoftwareFeatureManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwareTenantManagementBlazorServerModule),
    typeof(SmartSoftwareTenantManagementApplicationModule),
    typeof(SmartSoftwareTenantManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwarePermissionManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwarePermissionManagementDomainIdentityModule),
    typeof(SmartSoftwarePermissionManagementApplicationModule),
    typeof(SmartSoftwareSettingManagementBlazorServerModule),
    typeof(SmartSoftwareSettingManagementApplicationModule),
    typeof(SmartSoftwareSettingManagementEntityFrameworkCoreModule),
    typeof(MyProjectNameBlazorServerModule)
)]
public class MyProjectNameBlazorHostModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(MyProjectNameResource),
                typeof(MyProjectNameDomainModule).Assembly,
                typeof(MyProjectNameDomainSharedModule).Assembly,
                typeof(MyProjectNameApplicationModule).Assembly,
                typeof(MyProjectNameApplicationContractsModule).Assembly,
                typeof(MyProjectNameBlazorHostModule).Assembly
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

        PreConfigure<SmartSoftwareAspNetCoreComponentsWebOptions>(options =>
        {
            options.IsBlazorWebApp = true;
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        // Add services to the container.
        context.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

        Configure<SmartSoftwareDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });

        Configure<SmartSoftwareMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        Configure<SmartSoftwareBundlingOptions>(options =>
        {
                // MVC UI
                options.StyleBundles.Configure(
                BasicThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );

                //BLAZOR UI
                options.StyleBundles.Configure(
                BlazorBasicThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/blazor-global-styles.css");
                        //You can remove the following line if you don't use Blazor CSS isolation for components
                        bundle.AddFiles(new BundleFile("/MyCompanyName.MyProjectName.Blazor.Server.Host.styles.css", true));
                }
            );
        });

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}MyCompanyName.MyProjectName.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}MyCompanyName.MyProjectName.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}MyCompanyName.MyProjectName.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}MyCompanyName.MyProjectName.Application", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MyProjectNameBlazorHostModule>(hostingEnvironment.ContentRootPath);
            });
        }

        context.Services.AddSmartSoftwareSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyProjectName API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italian"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português (Brasil)"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch"));
            options.Languages.Add(new LanguageInfo("es", "es", "Español"));
            options.Languages.Add(new LanguageInfo("el", "el", "Ελληνικά"));
        });

        Configure<SmartSoftwareMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();

        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new MyProjectNameMenuContributor());
        });

        Configure<SmartSoftwareRouterOptions>(options =>
        {
            options.AppAssembly = typeof(MyProjectNameBlazorHostModule).Assembly;
        });

            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
                options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ?? Array.Empty<string>());
            });

#if DEBUG
        context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
    }

    public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var env = context.GetEnvironment();
        var app = context.GetApplicationBuilder();

        app.UseSmartSoftwareRequestLocalization();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseSmartSoftwareOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAntiforgery();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseSmartSoftwareSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyProjectName API");
        });
        app.UseConfiguredEndpoints(builder =>
        {
            builder.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddAdditionalAssemblies(builder.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareRouterOptions>>().Value.AdditionalAssemblies.ToArray());
        });

        using (var scope = context.ServiceProvider.CreateScope())
        {
            await scope.ServiceProvider
                .GetRequiredService<IDataSeeder>()
                .SeedAsync();
        }
    }
}
