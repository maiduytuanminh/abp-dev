using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Prismjs;
using SmartSoftware.AutoMapper;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Modularity;
using SmartSoftware.UI.Navigation;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.Blogging.Bundling;
using SmartSoftware.Blogging.Files;
using SmartSoftware.Blogging.Localization;

namespace SmartSoftware.Blogging
{
    [DependsOn(
        typeof(BloggingApplicationContractsModule),
        typeof(SmartSoftwareAspNetCoreMvcUiBootstrapModule),
        typeof(SmartSoftwareAspNetCoreMvcUiBundlingModule),
        typeof(SmartSoftwareAutoMapperModule)
    )]
    public class BloggingWebModule : SmartSoftwareModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(BloggingResource), typeof(BloggingWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(BloggingWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BloggingWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<BloggingWebModule>();
            Configure<SmartSoftwareAutoMapperOptions>(options =>
            {
                options.AddProfile<SmartSoftwareBloggingWebAutoMapperProfile>(validate: true);
            });

            Configure<SmartSoftwareBundleContributorOptions>(options =>
            {
                options
                    .Extensions<PrismjsStyleBundleContributor>()
                    .Add<PrismjsStyleBundleContributorBloggingExtension>();

                options
                    .Extensions<PrismjsScriptBundleContributor>()
                    .Add<PrismjsScriptBundleContributorBloggingExtension>();
            });
            
            Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("blogNameConstraint", typeof(BloggingRouteConstraint));
            });
            
            Configure<BloggingUrlOptions>(options =>
            {
                var bundlingOptions = context.Services.GetRequiredService<IOptions<SmartSoftwareBundlingOptions>>().Value;
                if (bundlingOptions.Mode != BundlingMode.None)
                {
                    options.IgnoredPaths.Add(bundlingOptions.BundleFolderName);
                }
                
                options.IgnoredPaths.AddRange(new[] 
                {
                    "error", "ApplicationConfigurationScript", "ServiceProxyScript", "Languages/Switch",
                    "ApplicationLocalizationScript", "members"
                });
            });

            Configure<RazorPagesOptions>(options =>
            {
                var urlOptions = context.Services
                    .GetRequiredServiceLazy<IOptions<BloggingUrlOptions>>()
                    .Value.Value;

                var routePrefix = urlOptions.RoutePrefix;

                if (urlOptions.SingleBlogMode.Enabled)
                {
                    options.Conventions.AddPageRoute("/Blogs/Posts/Index", routePrefix);
                    options.Conventions.AddPageRoute("/Blogs/Posts/Detail", routePrefix + "{postUrl}");
                    options.Conventions.AddPageRoute("/Blogs/Posts/Edit", routePrefix + "posts/{postId}/edit");
                    options.Conventions.AddPageRoute("/Blogs/Posts/New", routePrefix + "posts/new");
                }
                else
                {
                    if (!routePrefix.IsNullOrWhiteSpace())
                    {
                        options.Conventions.AddPageRoute("/Blogs/Index", routePrefix);
                    }
                    options.Conventions.AddPageRoute("/Blogs/Posts/Index", routePrefix + "{blogShortName:blogNameConstraint}");
                    options.Conventions.AddPageRoute("/Blogs/Posts/Detail", routePrefix + "{blogShortName:blogNameConstraint}/{postUrl}");
                    options.Conventions.AddPageRoute("/Blogs/Posts/Edit", routePrefix + "{blogShortName}/posts/{postId}/edit");
                    options.Conventions.AddPageRoute("/Blogs/Posts/New", routePrefix + "{blogShortName}/posts/new");
                }
                
                options.Conventions.AddPageRoute("/Blogs/Members/Index", routePrefix + "members/{userName}");
            });

            Configure<DynamicJavaScriptProxyOptions>(options =>
            {
                options.DisableModule(BloggingRemoteServiceConsts.ModuleName);
            });

            Configure<SmartSoftwareAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(FileUploadInputDto));
            });
        }
    }
}
