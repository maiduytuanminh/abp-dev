﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Authorization;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Users;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.Contents;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.Tags;
using SmartSoftware.CmsKit.Users;

namespace SmartSoftware.CmsKit.Public.Blogs;

[RequiresFeature(CmsKitFeatures.BlogEnable)]
[RequiresGlobalFeature(typeof(BlogsFeature))]
public class BlogPostPublicAppService : CmsKitPublicAppServiceBase, IBlogPostPublicAppService
{
    protected IBlogRepository BlogRepository { get; }

    protected IBlogPostRepository BlogPostRepository { get; }

    protected ITagRepository TagRepository { get; }

    public BlogPostPublicAppService(
        IBlogRepository blogRepository,
        IBlogPostRepository blogPostRepository,
        ITagRepository tagRepository)
    {
        BlogRepository = blogRepository;
        BlogPostRepository = blogPostRepository;
        TagRepository = tagRepository;
    }

    public virtual async Task<BlogPostCommonDto> GetAsync(
        [NotNull] string blogSlug, [NotNull] string blogPostSlug)
    {
        var blog = await BlogRepository.GetBySlugAsync(blogSlug);

        var blogPost = await BlogPostRepository.GetBySlugAsync(blog.Id, blogPostSlug);

        return ObjectMapper.Map<BlogPost, BlogPostCommonDto>(blogPost);
    }

    public virtual async Task<PagedResultDto<BlogPostCommonDto>> GetListAsync([NotNull] string blogSlug, BlogPostGetListInput input)
    {
        var blog = await BlogRepository.GetBySlugAsync(blogSlug);

        var blogPosts = await BlogPostRepository.GetListAsync(null, blog.Id, input.AuthorId, input.TagId,
            BlogPostStatus.Published, input.MaxResultCount,
            input.SkipCount, input.Sorting);

        return new PagedResultDto<BlogPostCommonDto>(
            await BlogPostRepository.GetCountAsync(blogId: blog.Id, tagId: input.TagId,
                statusFilter: BlogPostStatus.Published, authorId: input.AuthorId),
            ObjectMapper.Map<List<BlogPost>, List<BlogPostCommonDto>>(blogPosts));
    }

    public virtual async Task<PagedResultDto<CmsUserDto>> GetAuthorsHasBlogPostsAsync(BlogPostFilteredPagedAndSortedResultRequestDto input)
    {
        var authors = await BlogPostRepository.GetAuthorsHasBlogPostsAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);
        var authorDtos = ObjectMapper.Map<List<CmsUser>, List<CmsUserDto>>(authors);

        return new PagedResultDto<CmsUserDto>(
            await BlogPostRepository.GetAuthorsHasBlogPostsCountAsync(input.Filter),
            authorDtos);
    }

    public virtual async Task<CmsUserDto> GetAuthorHasBlogPostAsync(Guid id)
    {
        var author = await BlogPostRepository.GetAuthorHasBlogPostAsync(id);

        return ObjectMapper.Map<CmsUser, CmsUserDto>(author);
    }

    [Authorize]
    public virtual async Task DeleteAsync(Guid id)
    {
        var rating = await BlogPostRepository.GetAsync(id);

        if (rating.CreatorId != CurrentUser.GetId())
        {
            throw new SmartSoftwareAuthorizationException();
        }

        await BlogPostRepository.DeleteAsync(id);
    }

    public async Task<string> GetTagNameAsync([NotNull] Guid tagId)
    {
        var tag = await TagRepository.GetAsync(tagId);

        return tag.Name;
    }
}
