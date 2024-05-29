using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Application.Services;

namespace SmartSoftware.Blogging.Posts
{
    public interface IPostAppService : IApplicationService
    {
        Task<ListResultDto<PostWithDetailsDto>> GetListByBlogIdAndTagNameAsync(Guid blogId, string tagName);

        Task<ListResultDto<PostWithDetailsDto>> GetTimeOrderedListAsync(Guid blogId);

        Task<PostWithDetailsDto> GetForReadingAsync(GetPostInput input);

        Task<PostWithDetailsDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<PostWithDetailsDto> CreateAsync(CreatePostDto input);

        Task<PostWithDetailsDto> UpdateAsync(Guid id, UpdatePostDto input);
        
        Task<List<PostWithDetailsDto>> GetListByUserIdAsync(Guid userId);
        
        Task<List<PostWithDetailsDto>> GetLatestBlogPostsAsync(Guid blogId, int count);
    }
}
