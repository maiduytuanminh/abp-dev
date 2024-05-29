using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Features;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.Public.Pages;

namespace SmartSoftware.CmsKit.Public.Web.Pages;

public class CmsKitHomePageRouteValueTransformer : DynamicRouteValueTransformer, ITransientDependency
{
    protected IFeatureChecker FeatureChecker { get; }

    protected IPagePublicAppService PagePublicAppService { get; }

    public CmsKitHomePageRouteValueTransformer(IFeatureChecker featureChecker, IPagePublicAppService pagePublicAppService)
    {
        FeatureChecker = featureChecker;
        PagePublicAppService = pagePublicAppService;
    }

    public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
    {
        if (await FeatureChecker.IsEnabledAsync(CmsKitFeatures.PageEnable))
        {
            var page = await PagePublicAppService.FindDefaultHomePageAsync();
            if (page is not null)
            {
                values = new RouteValueDictionary();

                values["page"] = "/Public/CmsKit/Pages/Index";
                values["slug"] = page.Slug;
            }
        }

        return values;
    }
}
