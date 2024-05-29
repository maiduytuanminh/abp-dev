using System;
using System.Threading.Tasks;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Application.Services;

namespace SmartSoftware.CmsKit.Admin.Comments;

public interface ICommentAdminAppService : IApplicationService
{
    Task<PagedResultDto<CommentWithAuthorDto>> GetListAsync(CommentGetListInput input);

    Task<CommentWithAuthorDto> GetAsync(Guid id);

    Task DeleteAsync(Guid id);
}
