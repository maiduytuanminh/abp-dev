using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartSoftware;
using SmartSoftware.Account;
using SmartSoftware.Account.Web;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AspNetCore.Serilog;
using SmartSoftware.Autofac;
using SmartSoftware.Data;
using SmartSoftware.FeatureManagement;
using SmartSoftware.Identity;
using SmartSoftware.Identity.Web;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.PermissionManagement;
using SmartSoftware.PermissionManagement.HttpApi;
using SmartSoftware.PermissionManagement.Identity;
using SmartSoftware.Swashbuckle;
using SmartSoftware.TenantManagement;
using SmartSoftware.TenantManagement.Web;
using SmartSoftware.Threading;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.CmsKit.Admin.Web;
using SmartSoftware.CmsKit.Comments;
using SmartSoftware.CmsKit.MediaDescriptors;
using SmartSoftware.CmsKit.MultiTenancy;
using SmartSoftware.CmsKit.Public.Web;
using SmartSoftware.CmsKit.Ratings;
using SmartSoftware.CmsKit.Reactions;
using SmartSoftware.CmsKit.Tags;
using SmartSoftware.CmsKit.Web;
using SmartSoftware.CmsKit.Web.Contents;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartSoftware.DependencyInjection;
using SmartSoftware.CmsKit.Public.Pages;


#if EntityFrameworkCore
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.TenantManagement.EntityFrameworkCore;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.FeatureManagement.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.SqlServer;
using SmartSoftware.BlobStoring.Database.EntityFrameworkCore;
using SmartSoftware.CmsKit.EntityFrameworkCore;
using SmartSoftware.AuditLogging.EntityFrameworkCore;
#elif MongoDB
using SmartSoftware.SettingManagement.MongoDB;
using SmartSoftware.TenantManagement.MongoDB;
using SmartSoftware.Identity.MongoDB;
using SmartSoftware.PermissionManagement.MongoDB;
using SmartSoftware.FeatureManagement.MongoDB;
using SmartSoftware.BlobStoring.Database.MongoDB;
using SmartSoftware.AuditLogging.MongoDB;
using SmartSoftware.CmsKit.MongoDB;
#endif

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(CmsKitWebModule),
    typeof(CmsKitApplicationModule),
    typeof(CmsKitHttpApiModule),
#if EntityFrameworkCore
    typeof(CmsKitEntityFrameworkCoreModule),
    typeof(SmartSoftwareAuditLoggingEntityFrameworkCoreModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqlServerModule),
    typeof(SmartSoftwareSettingManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwarePermissionManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwareIdentityEntityFrameworkCoreModule),
    typeof(SmartSoftwareFeatureManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwareTenantManagementEntityFrameworkCoreModule),
    typeof(BlobStoringDatabaseEntityFrameworkCoreModule),
#elif MongoDB
    typeof(CmsKitMongoDbModule),
    typeof(SmartSoftwareAuditLoggingMongoDbModule),
    typeof(SmartSoftwareSettingManagementMongoDbModule),
    typeof(SmartSoftwarePermissionManagementMongoDbModule),
    typeof(SmartSoftwareIdentityMongoDbModule),
    typeof(SmartSoftwareFeatureManagementMongoDbModule),
    typeof(SmartSoftwareTenantManagementMongoDbModule),
    typeof(BlobStoringDatabaseMongoDbModule),
#endif
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareAccountWebModule),
    typeof(SmartSoftwareAccountApplicationModule),
    typeof(SmartSoftwareAccountHttpApiModule),
    typeof(SmartSoftwarePermissionManagementApplicationModule),
    typeof(SmartSoftwarePermissionManagementHttpApiModule),
    typeof(SmartSoftwareIdentityWebModule),
    typeof(SmartSoftwareIdentityApplicationModule),
    typeof(SmartSoftwareIdentityHttpApiModule),
    typeof(SmartSoftwarePermissionManagementDomainIdentityModule),
    typeof(SmartSoftwareFeatureManagementApplicationModule),
    typeof(SmartSoftwareFeatureManagementHttpApiModule),
    typeof(SmartSoftwareFeatureManagementWebModule),
    typeof(SmartSoftwareTenantManagementWebModule),
    typeof(SmartSoftwareTenantManagementApplicationModule),
    typeof(SmartSoftwareTenantManagementHttpApiModule),
    typeof(SmartSoftwareAspNetCoreMvcUiBasicThemeModule),
    typeof(SmartSoftwareAspNetCoreSerilogModule),
    typeof(SmartSoftwareSwashbuckleModule)
)]
public class CmsKitWebUnifiedModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        FeatureConfigurer.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        ConfigureCmsKit();

#if EntityFrameworkCore
        context.Services.AddDbContext<UnifiedDbContext>();
        Configure<SmartSoftwareDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });
#endif

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<SmartSoftwareAspNetCoreMvcUiThemeSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework/src{0}SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared", Path.DirectorySeparatorChar)));

                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.CmsKit.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.CmsKit.Domain", Path.DirectorySeparatorChar)));

                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitCommonWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.CmsKit.Common.Web", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitPublicWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.CmsKit.Public.Web", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitAdminWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.CmsKit.Admin.Web", Path.DirectorySeparatorChar)));

                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.CmsKit.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.CmsKit.Application", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.CmsKit.Web", Path.DirectorySeparatorChar)));
            });
        }

        context.Services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CmsKit API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi"));
            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português (Brasil)"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("el", "el", "Ελληνικά"));
        });

        Configure<SmartSoftwareMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        Configure<CmsKitContentWidgetOptions>(options =>
        {
            options.AddWidget("Today", "CmsToday", "Format");
        });
    }

    private void ConfigureCmsKit()
    {
        Configure<CmsKitTagOptions>(options =>
        {
            options.EntityTypes.Add(new TagEntityTypeDefiniton("quote"));
        });

        Configure<CmsKitCommentOptions>(options =>
        {
            options.EntityTypes.Add(new CommentEntityTypeDefinition("quote"));
            options.IsRecaptchaEnabled = true;
            options.AllowedExternalUrls = new Dictionary<string, List<string>>
            {
                {
                    "quote",
                    new List<string>
                    {
                        "https://smartsoftware.io/"
                    }
                }
            };
        });

        Configure<CmsKitMediaOptions>(options =>
        {
            options.EntityTypes.Add(new MediaDescriptorDefinition("quote"));
        });

        Configure<CmsKitReactionOptions>(options =>
        {
            options.EntityTypes.Add(
                new ReactionEntityTypeDefinition("quote",
                reactions: new[]
                {
                        new ReactionDefinition(StandardReactions.ThumbsUp),
                        new ReactionDefinition(StandardReactions.ThumbsDown),
                }));
        });

        Configure<CmsKitRatingOptions>(options =>
        {
            options.EntityTypes.Add(new RatingEntityTypeDefinition("quote"));
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
        else
        {
            app.UseErrorPage();
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseSmartSoftwareRequestLocalization();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSmartSoftwareSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");
        });

        app.UseAuditing();
        app.UseSmartSoftwareSerilogEnrichers();

        app.UseConfiguredEndpoints();

        using (var scope = context.ServiceProvider.CreateScope())
        {
            AsyncHelper.RunSync(async () =>
            {
                await scope.ServiceProvider
                    .GetRequiredService<IDataSeeder>()
                    .SeedAsync();
            });
        }
    }
}