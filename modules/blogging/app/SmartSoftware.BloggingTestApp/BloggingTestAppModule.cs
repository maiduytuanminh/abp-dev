//#define MONGODB

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using SmartSoftware;
using SmartSoftware.Account;
using SmartSoftware.Account.Web;
using SmartSoftware.AspNetCore.Mvc.UI;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AspNetCore.Mvc.UI.Theming;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Autofac;
using SmartSoftware.BlobStoring;
using SmartSoftware.BlobStoring.Database;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Identity;
using SmartSoftware.Identity.Web;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement;
using SmartSoftware.PermissionManagement.HttpApi;
using SmartSoftware.PermissionManagement.Identity;
using SmartSoftware.Threading;
using SmartSoftware.UI;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.Blogging;
using SmartSoftware.Blogging.Admin;
using SmartSoftware.Blogging.Files;
using SmartSoftware.BloggingTestApp.EntityFrameworkCore;

namespace SmartSoftware.BloggingTestApp
{
    [DependsOn(
        typeof(BloggingWebModule),
        typeof(BloggingApplicationModule),
        typeof(BloggingHttpApiModule),
        typeof(BloggingAdminWebModule),
        typeof(BloggingAdminHttpApiModule),
        typeof(BloggingAdminApplicationModule),
#if MONGODB
               typeof(BloggingTestAppMongoDbModule),
#else
        typeof(BloggingTestAppEntityFrameworkCoreModule),
#endif
        typeof(SmartSoftwareAccountWebModule),
        typeof(SmartSoftwareAccountHttpApiModule),
        typeof(SmartSoftwareAccountApplicationModule),
        typeof(SmartSoftwareIdentityWebModule),
        typeof(SmartSoftwareIdentityHttpApiModule),
        typeof(SmartSoftwareIdentityApplicationModule),
        typeof(SmartSoftwarePermissionManagementDomainIdentityModule),
        typeof(SmartSoftwarePermissionManagementApplicationModule),
        typeof(SmartSoftwarePermissionManagementHttpApiModule),
        typeof(BlobStoringDatabaseDomainModule),
        typeof(SmartSoftwareAutofacModule),
        typeof(SmartSoftwareAspNetCoreMvcUiBasicThemeModule)
    )]
    public class BloggingTestAppModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<BloggingUrlOptions>(options =>
            {
                options.RoutePrefix = null;
                options.SingleBlogMode.Enabled = true;
            });

            Configure<SmartSoftwareDbConnectionOptions>(options =>
            {
#if MONGODB
                const string connStringName = "MongoDb";
#else
                const string connStringName = "SqlServer";
#endif
                options.ConnectionStrings.Default = configuration.GetConnectionString(connStringName);
            });

#if !MONGODB
            Configure<SmartSoftwareDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });
#endif
            if (hostingEnvironment.IsDevelopment())
            {
                Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<SmartSoftwareUiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}SmartSoftware.UI", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<SmartSoftwareAspNetCoreMvcUiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}SmartSoftware.AspNetCore.Mvc.UI", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<SmartSoftwareAspNetCoreMvcUiBootstrapModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}SmartSoftware.AspNetCore.Mvc.UI.Bootstrap", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<SmartSoftwareAspNetCoreMvcUiThemeSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}framework{0}src{0}SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<SmartSoftwareAspNetCoreMvcUiBasicThemeModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}..{0}modules{0}basic-theme{0}src{0}SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<BloggingDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.Blogging.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<BloggingWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.Blogging.Web", Path.DirectorySeparatorChar)));
                });
            }

            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Blogging API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                });

            var cultures = new List<CultureInfo>
            {
                new CultureInfo("cs"),
                new CultureInfo("en"),
                new CultureInfo("tr"),
                new CultureInfo("zh-Hans")
            };

            Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            Configure<SmartSoftwareThemingOptions>(options =>
            {
                options.DefaultThemeName = BasicTheme.Name;
            });

            Configure<SmartSoftwareBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(container =>
                {
                    container.UseDatabase();
                });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            if (context.GetEnvironment().IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSmartSoftwareRequestLocalization();

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
