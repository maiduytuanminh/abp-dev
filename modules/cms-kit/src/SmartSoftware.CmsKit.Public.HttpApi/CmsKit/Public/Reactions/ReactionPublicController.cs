﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;

namespace SmartSoftware.CmsKit.Public.Reactions;

[RequiresFeature(CmsKitFeatures.ReactionEnable)]
[RequiresGlobalFeature(typeof(ReactionsFeature))]
[RemoteService(Name = CmsKitPublicRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/reactions")]
public class ReactionPublicController : CmsKitPublicControllerBase, IReactionPublicAppService
{
    protected IReactionPublicAppService ReactionPublicAppService { get; }

    public ReactionPublicController(IReactionPublicAppService reactionPublicAppService)
    {
        ReactionPublicAppService = reactionPublicAppService;
    }

    [HttpGet]
    [Route("{entityType}/{entityId}")]
    public virtual Task<ListResultDto<ReactionWithSelectionDto>> GetForSelectionAsync(string entityType, string entityId)
    {
        return ReactionPublicAppService.GetForSelectionAsync(entityType, entityId);
    }

    [HttpPut]
    [Route("{entityType}/{entityId}/{reaction}")]
    public virtual Task CreateAsync(string entityType, string entityId, string reaction)
    {
        return ReactionPublicAppService.CreateAsync(entityType, entityId, reaction);
    }

    [HttpDelete]
    [Route("{entityType}/{entityId}/{reaction}")]
    public virtual Task DeleteAsync(string entityType, string entityId, string reaction)
    {
        return ReactionPublicAppService.DeleteAsync(entityType, entityId, reaction);
    }
}