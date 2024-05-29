﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.Public.Web.Pages;

namespace SmartSoftware.CmsKit.Public.Web;

public static class CmsKitPagesConfigurationExtensions
{
    /// <summary>
    /// Maps CMS Kit to the routing.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="order">The matching order for the dynamic route. Lower is prior.</param>
    public static IEndpointRouteBuilder MapCmsPageRoute(this IEndpointRouteBuilder builder, int? order = null)
    {
        if (!GlobalFeatureManager.Instance.IsEnabled<PagesFeature>())
        {
            return builder;
        }

        builder
            .MapDynamicPageRoute<CmsKitHomePageRouteValueTransformer>("/", state: null, order: int.MinValue);

        if (order is null)
        {
            builder
                .MapDynamicPageRoute<CmsKitPageRouteValueTransformer>("{**slug}");
        }
        else
        {
            builder
                .MapDynamicPageRoute<CmsKitPageRouteValueTransformer>("{**slug}", state: default, order: order.Value);
        }

        return builder;
    }
}
