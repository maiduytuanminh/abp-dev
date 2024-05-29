using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
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

namespace SmartSoftware.CmsKit.EntityFrameworkCore;

[ConnectionStringName(SmartSoftwareCmsKitDbProperties.ConnectionStringName)]
public interface ICmsKitDbContext : IEfCoreDbContext
{
    DbSet<Comment> Comments { get; }
    DbSet<CmsUser> User { get; }
    DbSet<UserReaction> Reactions { get; }
    DbSet<Rating> Ratings { get; }
    DbSet<Tag> Tags { get; }
    DbSet<EntityTag> EntityTags { get; }
    DbSet<Page> Pages { get; }
    DbSet<Blog> Blogs { get; }
    DbSet<BlogPost> BlogPosts { get; }
    DbSet<BlogFeature> BlogFeatures { get; }
    DbSet<MediaDescriptor> MediaDescriptors { get; }
    DbSet<MenuItem> MenuItems { get; }
    DbSet<GlobalResource> GlobalResources { get; }
}
