using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using SmartSoftware;
using SmartSoftware.Account.Web;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AspNetCore.Mvc.UI.Theming;
using SmartSoftware.Autofac;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Identity;
using SmartSoftware.Identity.Web;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement;
using SmartSoftware.PermissionManagement.Identity;
using SmartSoftware.Threading;
using SmartSoftware.UI;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.Docs;
using SmartSoftware.Docs.Admin;
using SmartSoftware.Docs.Localization;
using SmartSoftwareDocs.EntityFrameworkCore;
using Localization.Resources.SmartSoftwareUi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using SmartSoftware.Account;
using SmartSoftware.PermissionManagement.HttpApi;
using SmartSoftware.Validation.Localization;
using SmartSoftware.Docs.Documents.FullSearch.Elastic;
using SmartSoftware.Caching.StackExchangeRedis;

namespace SmartSoftwareDocs.Web
{
    [DependsOn(
        typeof(DocsWebModule),
        typeof(DocsAdminWebModule),
        typeof(DocsApplicationModule),
        typeof(DocsHttpApiModule),
        typeof(DocsAdminApplicationModule),
        typeof(DocsAdminHttpApiModule),
        typeof(SmartSoftwareDocsEntityFrameworkCoreModule),
        typeof(SmartSoftwareAutofacModule),
        typeof(SmartSoftwareAccountWebModule),
        typeof(SmartSoftwareAccountApplicationModule),
        typeof(SmartSoftwareAccountHttpApiModule),
        typeof(SmartSoftwareIdentityWebModule),
        typeof(SmartSoftwareIdentityApplicationModule),
        typeof(SmartSoftwareIdentityHttpApiModule),
        typeof(SmartSoftwarePermissionManagementDomainIdentityModule),
        typeof(SmartSoftwarePermissionManagementApplicationModule),
        typeof(SmartSoftwarePermissionManagementHttpApiModule),
        typeof(SmartSoftwareAspNetCoreMvcUiBasicThemeModule)
        ,typeof(SmartSoftwareCachingStackExchangeRedisModule)
    )]
    public class SmartSoftwareDocsWebModule : SmartSoftwareModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(DocsResource), typeof(SmartSoftwareDocsWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<DocsUiOptions>(options =>
            {
                options.RoutePrefix = null;
            });

            Configure<DocsElasticSearchOptions>(options =>
            {
                options.Enable = true;
            });

            Configure<SmartSoftwareDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = configuration["ConnectionString"];
            });

            Configure<SmartSoftwareDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<SmartSoftwareUiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}SmartSoftware.UI", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<SmartSoftwareAspNetCoreMvcUiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}SmartSoftware.AspNetCore.Mvc.UI", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<SmartSoftwareAspNetCoreMvcUiBootstrapModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}SmartSoftware.AspNetCore.Mvc.UI.Bootstrap", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<SmartSoftwareAspNetCoreMvcUiThemeSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<SmartSoftwareAspNetCoreMvcUiBasicThemeModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}modules{0}basic-theme{0}src{0}SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<DocsDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.Docs.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<DocsWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.Docs.Web", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<DocsWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.Docs.Admin.Web", Path.DirectorySeparatorChar)));
                });
            }

            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Docs API",
                        Version = "v1"
                    });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                });

            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<SmartSoftwareDocsWebModule>("SmartSoftwareDocs.Web");
            });

            Configure<SmartSoftwareLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
                options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
                options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi"));
                options.Languages.Add(new LanguageInfo("is", "is", "Icelandic"));
                options.Languages.Add(new LanguageInfo("it", "it", "Italiano"));
                options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
                options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
                options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("el", "el", "Ελληνικά"));

                options.Resources
                    .Get<DocsResource>()
                    .AddBaseTypes(typeof(SmartSoftwareValidationResource))
                    .AddBaseTypes(typeof(SmartSoftwareUiResource))
                    .AddVirtualJson("/Localization/Resources/SmartSoftwareDocs/Web");
            });

            Configure<SmartSoftwareThemingOptions>(options =>
            {
                options.DefaultThemeName = BasicTheme.Name;
            });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AddPageRoute("/Error", "error/{statusCode}");
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSmartSoftwareRequestLocalization();
            app.UseSmartSoftwareSecurityHeaders();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");
            });

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            //app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

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
}
