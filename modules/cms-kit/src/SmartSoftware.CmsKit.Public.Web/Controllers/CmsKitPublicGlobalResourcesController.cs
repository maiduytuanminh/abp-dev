﻿using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Caching;
using SmartSoftware.CmsKit.GlobalResources;
using SmartSoftware.CmsKit.Public.GlobalResources;

namespace SmartSoftware.CmsKit.Public.Web.Controllers;

[Route("cms-kit/global-resources")]
public class CmsKitPublicGlobalResourcesController : CmsKitPublicControllerBase
{
    private readonly IGlobalResourcePublicAppService _globalResourcePublicAppService;
    private readonly IDistributedCache<GlobalResourceDto> _resourceCache;

    public CmsKitPublicGlobalResourcesController(
        IGlobalResourcePublicAppService globalResourcePublicAppService, 
        IDistributedCache<GlobalResourceDto> resourceCache)
    {
        _globalResourcePublicAppService = globalResourcePublicAppService;
        _resourceCache = resourceCache;
    }
    
    [HttpGet]
    [Route("style")]
    public virtual async Task<IActionResult> GetGlobalStyleAsync()
    {
        var style = await _resourceCache.GetOrAddAsync(
            GlobalResourceConsts.GlobalStyleName, //Cache key
            async () => await _globalResourcePublicAppService.GetGlobalStyleAsync(),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(2)
            }
        );
        
        return new FileContentResult(
            Encoding.ASCII.GetBytes(style.Value), 
            "text/css");
    }
    
    [HttpGet]
    [Route("script")]
    public virtual async Task<IActionResult> GetGlobalScriptAsync()
    {
        var script = await _resourceCache.GetOrAddAsync(
            GlobalResourceConsts.GlobalScriptName, //Cache key
            async () => await _globalResourcePublicAppService.GetGlobalScriptAsync(),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(2)
            }
        );
        
        return new FileContentResult(
            Encoding.ASCII.GetBytes(script.Value), 
            "application/javascript");
    }
}