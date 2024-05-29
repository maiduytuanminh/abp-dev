using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.Posts;
using SmartSoftware.Blogging.Tagging;
using SmartSoftware.Blogging.Users;

namespace SmartSoftware.Blogging.EntityFrameworkCore
{
    [IgnoreMultiTenancy]
    [ConnectionStringName(SmartSoftwareBloggingDbProperties.ConnectionStringName)]
    public interface IBloggingDbContext : IEfCoreDbContext
    {
        DbSet<BlogUser> Users { get; }

        DbSet<Blog> Blogs { get; }

        DbSet<Post> Posts { get; }

        DbSet<Comment> Comments { get; }

        DbSet<PostTag> PostTags { get; }

        DbSet<Tag> Tags { get; }
    }
}
