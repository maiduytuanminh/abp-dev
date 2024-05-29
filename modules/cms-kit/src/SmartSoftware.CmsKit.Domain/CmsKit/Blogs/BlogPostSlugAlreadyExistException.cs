﻿using System;
using SmartSoftware;

namespace SmartSoftware.CmsKit.Blogs;

public class BlogPostSlugAlreadyExistException : BusinessException
{
    public BlogPostSlugAlreadyExistException(Guid blogId, string slug)
    {
        Slug = slug;
        BlogId = blogId;

        Code = CmsKitErrorCodes.BlogPosts.SlugAlreadyExist;

        WithData(nameof(Slug), Slug);
        WithData(nameof(BlogId), BlogId);
    }

    public virtual string Slug { get; }

    public virtual Guid BlogId { get; }
}
