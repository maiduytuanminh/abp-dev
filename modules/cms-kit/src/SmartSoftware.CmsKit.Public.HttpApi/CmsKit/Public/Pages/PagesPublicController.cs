﻿using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Contents;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;

namespace SmartSoftware.CmsKit.Public.Pages;

[RequiresFeature(CmsKitFeatures.PageEnable)]
[RequiresGlobalFeature(typeof(PagesFeature))]
[RemoteService(Name = CmsKitPublicRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/pages")]
public class PagesPublicController : CmsKitPublicControllerBase, IPagePublicAppService
{
    protected IPagePublicAppService PageAppService { get; }

    public PagesPublicController(IPagePublicAppService pageAppService)
    {
        PageAppService = pageAppService;
    }

    [HttpGet]
    [Route("by-slug")]
    public virtual Task<PageDto> FindBySlugAsync([FromQuery]string slug)
    {
        return PageAppService.FindBySlugAsync(slug);
    }

    [HttpGet]
    [Route("home")]
    public virtual Task<PageDto> FindDefaultHomePageAsync()
    {
        return PageAppService.FindDefaultHomePageAsync();
    }

    [HttpGet]
    [Route("exist")]
    public virtual Task<bool> DoesSlugExistAsync([NotNull][FromQuery] string slug)
    {
        return PageAppService.DoesSlugExistAsync(slug);
    }
}
