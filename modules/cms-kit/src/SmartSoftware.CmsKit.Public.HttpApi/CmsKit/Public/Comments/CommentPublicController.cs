﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;

namespace SmartSoftware.CmsKit.Public.Comments;

[RequiresFeature(CmsKitFeatures.CommentEnable)]
[RequiresGlobalFeature(typeof(CommentsFeature))]
[RemoteService(Name = CmsKitPublicRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/comments")]
public class CommentPublicController : CmsKitPublicControllerBase, ICommentPublicAppService
{
    public ICommentPublicAppService CommentPublicAppService { get; }

    public CommentPublicController(ICommentPublicAppService commentPublicAppService)
    {
        CommentPublicAppService = commentPublicAppService;
    }

    [HttpGet]
    [Route("{entityType}/{entityId}")]
    public Task<ListResultDto<CommentWithDetailsDto>> GetListAsync(string entityType, string entityId)
    {
        return CommentPublicAppService.GetListAsync(entityType, entityId);
    }

    [HttpPost]
    [Route("{entityType}/{entityId}")]
    public Task<CommentDto> CreateAsync(string entityType, string entityId, CreateCommentInput input)
    {
        return CommentPublicAppService.CreateAsync(entityType, entityId, input);
    }

    [HttpPut]
    [Route("{id}")]
    public Task<CommentDto> UpdateAsync(Guid id, UpdateCommentInput input)
    {
        return CommentPublicAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return CommentPublicAppService.DeleteAsync(id);
    }
}
