using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.Comments;
using SmartSoftware.CmsKit.GlobalResources;
using SmartSoftware.CmsKit.MediaDescriptors;
using SmartSoftware.CmsKit.Menus;
using SmartSoftware.CmsKit.Pages;
using SmartSoftware.CmsKit.Ratings;
using SmartSoftware.CmsKit.Reactions;
using SmartSoftware.CmsKit.Tags;
using SmartSoftware.CmsKit.Users;
using Tag = SmartSoftware.CmsKit.Tags.Tag;

namespace SmartSoftware.CmsKit.MongoDB;

[ConnectionStringName(SmartSoftwareCmsKitDbProperties.ConnectionStringName)]
public interface ICmsKitMongoDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<UserReaction> UserReactions { get; }

    IMongoCollection<Comment> Comments { get; }

    IMongoCollection<CmsUser> CmsUsers { get; }

    IMongoCollection<Rating> Ratings { get; }

    IMongoCollection<Tag> Tags { get; }

    IMongoCollection<EntityTag> EntityTags { get; }

    IMongoCollection<Page> Pages { get; }

    IMongoCollection<Blog> Blogs { get; }

    IMongoCollection<BlogPost> BlogPosts { get; }

    IMongoCollection<BlogFeature> BlogFeatures { get; }

    IMongoCollection<MediaDescriptor> MediaDescriptors { get; }

    IMongoCollection<MenuItem> MenuItems { get; }

    IMongoCollection<GlobalResource> GlobalResources { get; }
}
