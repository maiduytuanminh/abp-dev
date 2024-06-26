﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Features;
using SmartSoftware.CmsKit.Features;

namespace SmartSoftware.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.GlobalResources.Script;

public class GlobalScriptViewComponent : SmartSoftwareViewComponent
{
    protected IFeatureChecker FeatureChecker { get; }

    public GlobalScriptViewComponent(IFeatureChecker featureChecker)
    {
        FeatureChecker = featureChecker;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (!await FeatureChecker.IsEnabledAsync(CmsKitFeatures.GlobalResourceEnable))
        {
            return Content(string.Empty);
        }

        return View("~/Pages/CmsKit/Shared/Components/GlobalResources/Script/Default.cshtml");
    }
}