using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.Permissions;

namespace SmartSoftware.CmsKit.Admin.Comments;

[RequiresFeature(CmsKitFeatures.CommentEnable)]
[Authorize(CmsKitAdminPermissions.Comments.Default)]
[RequiresGlobalFeature(typeof(CommentsFeature))]
[RemoteService(Name = CmsKitAdminRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitAdminRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-admin/comments")]
public class CommentAdminController : CmsKitAdminController, ICommentAdminAppService
{
    protected ICommentAdminAppService CommentAdminAppService { get; }

    public CommentAdminController(ICommentAdminAppService commentAdminAppService)
    {
        CommentAdminAppService = commentAdminAppService;
    }

    [HttpGet]
    public virtual Task<PagedResultDto<CommentWithAuthorDto>> GetListAsync(CommentGetListInput input)
    {
        return CommentAdminAppService.GetListAsync(input);
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<CommentWithAuthorDto> GetAsync(Guid id)
    {
        return CommentAdminAppService.GetAsync(id);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(CmsKitAdminPermissions.Comments.Delete)]
    public virtual Task DeleteAsync(Guid id)
    {
        return CommentAdminAppService.DeleteAsync(id);
    }
}
