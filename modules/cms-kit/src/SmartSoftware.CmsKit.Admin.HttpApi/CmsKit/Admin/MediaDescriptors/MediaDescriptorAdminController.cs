﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.GlobalFeatures;

namespace SmartSoftware.CmsKit.Admin.MediaDescriptors;

[RequiresGlobalFeature(typeof(MediaFeature))]
[RemoteService(Name = CmsKitAdminRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitAdminRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-admin/media")]
public class MediaDescriptorAdminController : CmsKitAdminController, IMediaDescriptorAdminAppService
{
    protected readonly IMediaDescriptorAdminAppService MediaDescriptorAdminAppService;

    public MediaDescriptorAdminController(IMediaDescriptorAdminAppService mediaDescriptorAdminAppService)
    {
        MediaDescriptorAdminAppService = mediaDescriptorAdminAppService;
    }

    [HttpPost]
    [Route("{entityType}")]
    public virtual Task<MediaDescriptorDto> CreateAsync(string entityType, CreateMediaInputWithStream inputStream)
    {
        return MediaDescriptorAdminAppService.CreateAsync(entityType, inputStream);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return MediaDescriptorAdminAppService.DeleteAsync(id);
    }
}
