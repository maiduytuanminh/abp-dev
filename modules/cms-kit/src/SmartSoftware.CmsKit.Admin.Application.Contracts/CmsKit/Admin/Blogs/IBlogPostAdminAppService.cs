using System;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.CmsKit.Admin.Blogs;

public interface IBlogPostAdminAppService
    : ICrudAppService<
        BlogPostDto,
        BlogPostListDto,
        Guid,
        BlogPostGetListInput,
        CreateBlogPostDto,
        UpdateBlogPostDto>
{
    Task PublishAsync(Guid id);

    Task DraftAsync(Guid id);

    Task<BlogPostDto> CreateAndPublishAsync(CreateBlogPostDto input);

    Task SendToReviewAsync(Guid id);

    Task<BlogPostDto> CreateAndSendToReviewAsync(CreateBlogPostDto input);

    Task<bool> HasBlogPostWaitingForReviewAsync();
}
