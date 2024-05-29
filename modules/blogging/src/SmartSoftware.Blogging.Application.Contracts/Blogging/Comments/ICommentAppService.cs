using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;
using SmartSoftware.Blogging.Comments.Dtos;

namespace SmartSoftware.Blogging.Comments
{
    public interface ICommentAppService : IApplicationService
    {
        Task<List<CommentWithRepliesDto>> GetHierarchicalListOfPostAsync(Guid postId);

        Task<CommentWithDetailsDto> CreateAsync(CreateCommentDto input);

        Task<CommentWithDetailsDto> UpdateAsync(Guid id, UpdateCommentDto input);

        Task DeleteAsync(Guid id);
    }
}
