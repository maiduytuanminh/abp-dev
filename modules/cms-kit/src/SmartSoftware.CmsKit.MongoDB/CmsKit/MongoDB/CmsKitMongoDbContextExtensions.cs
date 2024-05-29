using SmartSoftware;
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

namespace SmartSoftware.CmsKit.MongoDB;

public static class CmsKitMongoDbContextExtensions
{
    public static void ConfigureCmsKit(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<CmsUser>(x =>
        {
            x.CollectionName = SmartSoftwareCmsKitDbProperties.DbTablePrefix + "Users";
        });

        builder.Entity<UserReaction>(x =>
        {
            x.CollectionName = SmartSoftwareCmsKitDbProperties.DbTablePrefix + "UserReactions";
        });

        builder.Entity<Comment>(x =>
        {
            x.CollectionName = SmartSoftwareCmsKitDbProperties.DbTablePrefix + "Comments";
        });

        builder.Entity<Rating>(x =>
        {
            x.CollectionName = SmartSoftwareCmsKitDbProperties.DbTablePrefix + "Ratings";
        });

        builder.Entity<Tag>(x =>
        {
            x.CollectionName = SmartSoftwareCmsKitDbProperties.DbTablePrefix + "Tags";
        });

        builder.Entity<EntityTag>(x =>
        {
            x.CollectionName = SmartSoftwareCmsKitDbProperties.DbTablePrefix + "EntityTags";
        });

        builder.Entity<Page>(x =>
        {
            x.CollectionName = SmartSoftwareCmsKitDbProperties.DbTablePrefix + "Pages";
        });

        builder.Entity<Blog>(x =>
        {
            x.CollectionName = SmartSoftwareCmsKitDbProperties.DbTablePrefix + "Blogs";
        });

        builder.Entity<BlogPost>(x =>
        {
            x.CollectionName = SmartSoftwareCmsKitDbProperties.DbTablePrefix + "BlogPosts";
        });

        builder.Entity<BlogFeature>(x =>
        {
            x.CollectionName = SmartSoftwareCmsKitDbProperties.DbTablePrefix + "BlogFeatures";
        });

        builder.Entity<MediaDescriptor>(x =>
        {
            x.CollectionName = SmartSoftwareCmsKitDbProperties.DbTablePrefix + "MediaDescriptors";
        });

        builder.Entity<MenuItem>(x =>
        {
            x.CollectionName = SmartSoftwareCmsKitDbProperties.DbTablePrefix + "MenuItems";
        });

        builder.Entity<GlobalResource>(x =>
        {
            x.CollectionName = SmartSoftwareCmsKitDbProperties.DbTablePrefix + "GlobalResources";
        });
    }
}
