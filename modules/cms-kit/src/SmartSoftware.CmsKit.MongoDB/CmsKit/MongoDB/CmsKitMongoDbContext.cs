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
public class CmsKitMongoDbContext : SmartSoftwareMongoDbContext, ICmsKitMongoDbContext
{
    public IMongoCollection<Comment> Comments => Collection<Comment>();

    public IMongoCollection<UserReaction> UserReactions => Collection<UserReaction>();

    public IMongoCollection<CmsUser> CmsUsers => Collection<CmsUser>();

    public IMongoCollection<Rating> Ratings => Collection<Rating>();

    public IMongoCollection<Tag> Tags => Collection<Tag>();

    public IMongoCollection<EntityTag> EntityTags => Collection<EntityTag>();

    public IMongoCollection<Page> Pages => Collection<Page>();

    public IMongoCollection<Blog> Blogs => Collection<Blog>();

    public IMongoCollection<BlogPost> BlogPosts => Collection<BlogPost>();

    public IMongoCollection<BlogFeature> BlogFeatures => Collection<BlogFeature>();

    public IMongoCollection<MediaDescriptor> MediaDescriptors => Collection<MediaDescriptor>();

    public IMongoCollection<MenuItem> MenuItems => Collection<MenuItem>();

    public IMongoCollection<GlobalResource> GlobalResources => Collection<GlobalResource>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureCmsKit();
    }
}
