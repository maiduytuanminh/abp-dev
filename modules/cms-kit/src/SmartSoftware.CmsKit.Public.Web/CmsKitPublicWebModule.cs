using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.Localization;
using SmartSoftware.AutoMapper;
using SmartSoftware.Caching;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Http.ProxyScripting.Generators.JQuery;
using SmartSoftware.Modularity;
using SmartSoftware.Ui.LayoutHooks;
using SmartSoftware.UI.Navigation;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.Localization;
using SmartSoftware.CmsKit.Public.Web.Menus;
using SmartSoftware.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.GlobalResources.Script;
using SmartSoftware.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.GlobalResources.Style;
using SmartSoftware.CmsKit.Web;

namespace SmartSoftware.CmsKit.Public.Web;

[DependsOn(
    typeof(CmsKitPublicApplicationContractsModule),
    typeof(CmsKitCommonWebModule)
)]
public class CmsKitPublicWebModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<SmartSoftwareMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(CmsKitResource),
                typeof(CmsKitPublicWebModule).Assembly,
                typeof(CmsKitPublicApplicationContractsModule).Assembly,
                typeof(CmsKitCommonApplicationContractsModule).Assembly
            );
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsKitPublicWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new CmsKitPublicMenuContributor());
            options.MainMenuNames.Add(CmsKitMenus.Public);
        });

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitPublicWebModule>("SmartSoftware.CmsKit.Public.Web");
        });

        context.Services.AddAutoMapperObjectMapper<CmsKitPublicWebModule>();

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddMaps<CmsKitPublicWebModule>(validate: true);
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(CmsKitPublicRemoteServiceConsts.ModuleName);
        });

        Configure<SmartSoftwareDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "CmsKit:";
        });

        if (GlobalFeatureManager.Instance.IsEnabled<PagesFeature>())
        {
            Configure<SmartSoftwareEndpointRouterOptions>(options =>
            {
                options.EndpointConfigureActions.Add(context =>
                {
                    context.Endpoints.MapCmsPageRoute();
                });
            });
        }
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        if (GlobalFeatureManager.Instance.IsEnabled<PagesFeature>())
        {
            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AddPageRoute(
                    "/Public/CmsKit/Blogs/Index",
                    CmsBlogsWebConsts.BlogsRoutePrefix.EnsureStartsWith('/') + @"/{blogSlug:minlength(1)}");

                options.Conventions.AddPageRoute(
                    "/Public/CmsKit/Blogs/BlogPost",
                    CmsBlogsWebConsts.BlogsRoutePrefix.EnsureStartsWith('/') + @"/{blogSlug}/{blogPostSlug:minlength(1)}");
            });
        }

        if (GlobalFeatureManager.Instance.IsEnabled<GlobalResourcesFeature>())
        {
            Configure<SmartSoftwareLayoutHookOptions>(options =>
            {
                options.Add(
                    LayoutHooks.Head.Last,
                    typeof(GlobalStyleViewComponent)
                );
                options.Add(
                    LayoutHooks.Body.Last,
                    typeof(GlobalScriptViewComponent)
                );
            });
        }
    }
}
