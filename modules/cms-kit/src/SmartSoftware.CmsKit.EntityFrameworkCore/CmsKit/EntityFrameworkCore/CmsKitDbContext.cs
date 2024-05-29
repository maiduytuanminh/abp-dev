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
public class CmsKitDbContext : SmartSoftwareDbContext<CmsKitDbContext>, ICmsKitDbContext
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CmsUser> User { get; set; }
    public DbSet<UserReaction> Reactions { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<EntityTag> EntityTags { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<BlogFeature> BlogFeatures { get; set; }
    public DbSet<MediaDescriptor> MediaDescriptors { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<GlobalResource> GlobalResources { get; set; }

    public CmsKitDbContext(DbContextOptions<CmsKitDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureCmsKit();
    }
}
