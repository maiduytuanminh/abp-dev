﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;

namespace SmartSoftware.CmsKit.Admin.Tags;

[RequiresFeature(CmsKitFeatures.TagEnable)]
[RequiresGlobalFeature(typeof(TagsFeature))]
[RemoteService(Name = CmsKitAdminRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitAdminRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-admin/entity-tags")]
public class EntityTagAdminController : CmsKitAdminController, IEntityTagAdminAppService
{
    protected IEntityTagAdminAppService EntityTagAdminAppService { get; }

    public EntityTagAdminController(IEntityTagAdminAppService entityTagAdminAppService)
    {
        EntityTagAdminAppService = entityTagAdminAppService;
    }

    [HttpPost]
    public Task AddTagToEntityAsync(EntityTagCreateDto input)
    {
        return EntityTagAdminAppService.AddTagToEntityAsync(input);
    }

    [HttpDelete]
    public Task RemoveTagFromEntityAsync(EntityTagRemoveDto input)
    {
        return EntityTagAdminAppService.RemoveTagFromEntityAsync(input);
    }

    [HttpPut]
    public Task SetEntityTagsAsync(EntityTagSetDto input)
    {
        return EntityTagAdminAppService.SetEntityTagsAsync(input);
    }
}
