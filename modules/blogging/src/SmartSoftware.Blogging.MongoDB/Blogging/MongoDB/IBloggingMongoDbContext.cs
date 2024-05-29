using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.Posts;
using SmartSoftware.Blogging.Users;

namespace SmartSoftware.Blogging.MongoDB
{
    [IgnoreMultiTenancy]
    [ConnectionStringName(SmartSoftwareBloggingDbProperties.ConnectionStringName)]
    public interface IBloggingMongoDbContext : ISmartSoftwareMongoDbContext
    {
        IMongoCollection<BlogUser> Users { get; }

        IMongoCollection<Blog> Blogs { get; }

        IMongoCollection<Post> Posts { get; }

        IMongoCollection<Tagging.Tag> Tags { get; }

        IMongoCollection<Comment> Comments { get; }

    }
}
