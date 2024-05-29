using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Application.Services;
using SmartSoftware.CmsKit.Contents;
using SmartSoftware.CmsKit.Users;

namespace SmartSoftware.CmsKit.Public.Blogs;

public interface IBlogPostPublicAppService : IApplicationService
{
    Task<PagedResultDto<BlogPostCommonDto>> GetListAsync([NotNull] string blogSlug, BlogPostGetListInput input);

    Task<BlogPostCommonDto> GetAsync([NotNull] string blogSlug, [NotNull] string blogPostSlug);

    Task<PagedResultDto<CmsUserDto>> GetAuthorsHasBlogPostsAsync(BlogPostFilteredPagedAndSortedResultRequestDto input);

    Task<CmsUserDto> GetAuthorHasBlogPostAsync(Guid id);

    Task DeleteAsync(Guid id);

    Task<string> GetTagNameAsync([NotNull] Guid tagId);
}
