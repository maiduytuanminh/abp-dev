﻿using Microsoft.EntityFrameworkCore;
using SmartSoftware;
using SmartSoftware.EntityFrameworkCore.Modeling;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Users.EntityFrameworkCore;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.Comments;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.GlobalResources;
using SmartSoftware.CmsKit.MediaDescriptors;
using SmartSoftware.CmsKit.Menus;
using SmartSoftware.CmsKit.Pages;
using SmartSoftware.CmsKit.Ratings;
using SmartSoftware.CmsKit.Reactions;
using SmartSoftware.CmsKit.Tags;
using SmartSoftware.CmsKit.Users;

namespace SmartSoftware.CmsKit.EntityFrameworkCore;

public static class CmsKitDbContextModelCreatingExtensions
{
    public static void ConfigureCmsKit(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        if (GlobalFeatureManager.Instance.IsEnabled<CmsUserFeature>())
        {
            builder.Entity<CmsUser>(b =>
            {
                b.ToTable(SmartSoftwareCmsKitDbProperties.DbTablePrefix + "Users", SmartSoftwareCmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();
                b.ConfigureSmartSoftwareUser();

                b.HasIndex(x => new { x.TenantId, x.UserName });
                b.HasIndex(x => new { x.TenantId, x.Email });

                b.ApplyObjectExtensionMappings();
            });
        }
        else
        {
            builder.Ignore<CmsUser>();
        }

        if (GlobalFeatureManager.Instance.IsEnabled<ReactionsFeature>())
        {
            builder.Entity<UserReaction>(b =>
            {
                b.ToTable(SmartSoftwareCmsKitDbProperties.DbTablePrefix + "UserReactions", SmartSoftwareCmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.EntityType).IsRequired().HasMaxLength(UserReactionConsts.MaxEntityTypeLength);
                b.Property(x => x.EntityId).IsRequired().HasMaxLength(UserReactionConsts.MaxEntityIdLength);
                b.Property(x => x.ReactionName).IsRequired().HasMaxLength(UserReactionConsts.MaxReactionNameLength);

                b.HasIndex(x => new { x.TenantId, x.EntityType, x.EntityId, x.ReactionName });
                b.HasIndex(x => new { x.TenantId, x.CreatorId, x.EntityType, x.EntityId, x.ReactionName });

                b.ApplyObjectExtensionMappings();
            });
        }
        else
        {
            builder.Ignore<UserReaction>();
        }

        if (GlobalFeatureManager.Instance.IsEnabled<CommentsFeature>())
        {
            builder.Entity<Comment>(b =>
            {
                b.ToTable(SmartSoftwareCmsKitDbProperties.DbTablePrefix + "Comments", SmartSoftwareCmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.EntityType).IsRequired().HasMaxLength(CommentConsts.MaxEntityTypeLength);
                b.Property(x => x.EntityId).IsRequired().HasMaxLength(CommentConsts.MaxEntityIdLength);
                b.Property(x => x.Text).IsRequired().HasMaxLength(CommentConsts.MaxTextLength);
                b.Property(x => x.RepliedCommentId);
                b.Property(x => x.Url).HasMaxLength(CommentConsts.MaxUrlLength);
                b.Property(x => x.IdempotencyToken).HasMaxLength(CommentConsts.MaxIdempotencyTokenLength);

                b.HasIndex(x => new { x.TenantId, x.EntityType, x.EntityId });
                b.HasIndex(x => new { x.TenantId, x.RepliedCommentId });

                b.ApplyObjectExtensionMappings();
            });
        }
        else
        {
            builder.Ignore<Comment>();
        }

        if (GlobalFeatureManager.Instance.IsEnabled<RatingsFeature>())
        {
            builder.Entity<Rating>(r =>
            {
                r.ToTable(SmartSoftwareCmsKitDbProperties.DbTablePrefix + "Ratings", SmartSoftwareCmsKitDbProperties.DbSchema);

                r.ConfigureByConvention();

                r.Property(x => x.StarCount).IsRequired();
                r.Property(x => x.EntityType).IsRequired().HasMaxLength(RatingConsts.MaxEntityTypeLength);
                r.Property(x => x.EntityId).IsRequired().HasMaxLength(RatingConsts.MaxEntityIdLength);

                r.HasIndex(x => new { x.TenantId, x.EntityType, x.EntityId, x.CreatorId });

                r.ApplyObjectExtensionMappings();
            });
        }
        else
        {
            builder.Ignore<Rating>();
        }

        if (GlobalFeatureManager.Instance.IsEnabled<TagsFeature>())
        {
            builder.Entity<Tag>(b =>
            {
                b.ToTable(SmartSoftwareCmsKitDbProperties.DbTablePrefix + "Tags", SmartSoftwareCmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.EntityType).IsRequired().HasMaxLength(TagConsts.MaxEntityTypeLength);
                b.Property(x => x.Name).IsRequired().HasMaxLength(TagConsts.MaxNameLength);

                b.HasIndex(x => new {
                    x.TenantId,
                    x.Name
                });

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<EntityTag>(b =>
            {
                b.ToTable(SmartSoftwareCmsKitDbProperties.DbTablePrefix + "EntityTags", SmartSoftwareCmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.HasKey(x => new { x.EntityId, x.TagId });

                b.Property(x => x.EntityId).IsRequired();
                b.Property(x => x.TagId).IsRequired();

                b.HasIndex(x => new { x.TenantId, x.EntityId, x.TagId });

                b.ApplyObjectExtensionMappings();
            });
        }
        else
        {
            builder.Ignore<EntityTag>();
            builder.Ignore<Tag>();
        }

        if (GlobalFeatureManager.Instance.IsEnabled<PagesFeature>())
        {
            builder.Entity<Page>(b =>
            {
                b.ToTable(SmartSoftwareCmsKitDbProperties.DbTablePrefix + "Pages", SmartSoftwareCmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Title).IsRequired().HasMaxLength(PageConsts.MaxTitleLength);
                b.Property(x => x.Slug).IsRequired().HasMaxLength(PageConsts.MaxSlugLength);
                b.Property(x => x.Content).HasMaxLength(PageConsts.MaxContentLength);

                b.HasIndex(x => new { x.TenantId, Url = x.Slug });

                b.ApplyObjectExtensionMappings();
            });
        }
        else
        {
            builder.Ignore<Page>();
        }

        if (GlobalFeatureManager.Instance.IsEnabled<BlogsFeature>())
        {
            builder.Entity<Blog>(b =>
            {
                b.ToTable(SmartSoftwareCmsKitDbProperties.DbTablePrefix + "Blogs", SmartSoftwareCmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(p => p.Name).IsRequired().HasMaxLength(BlogConsts.MaxNameLength);

                b.Property(p => p.Slug).IsRequired().HasMaxLength(BlogConsts.MaxSlugLength);

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<BlogPost>(b =>
            {
                b.ToTable(SmartSoftwareCmsKitDbProperties.DbTablePrefix + "BlogPosts", SmartSoftwareCmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(p => p.AuthorId).IsRequired();
                b.Property(p => p.Title).IsRequired().HasMaxLength(BlogPostConsts.MaxTitleLength);
                b.Property(p => p.Slug).IsRequired().HasMaxLength(BlogPostConsts.MaxSlugLength);
                b.Property(p => p.ShortDescription).HasMaxLength(BlogPostConsts.MaxShortDescriptionLength);
                b.Property(p => p.Content).HasMaxLength(BlogPostConsts.MaxContentLength);

                b.HasIndex(x => new { x.Slug, x.BlogId });

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<BlogFeature>(b =>
            {
                b.ToTable(SmartSoftwareCmsKitDbProperties.DbTablePrefix + "BlogFeatures", SmartSoftwareCmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(p => p.FeatureName).IsRequired().HasMaxLength(BlogFeatureConsts.MaxFeatureNameLenth);

                b.ApplyObjectExtensionMappings();
            });
        }
        else
        {
            builder.Ignore<Blog>();
            builder.Ignore<BlogPost>();
            builder.Ignore<BlogFeature>();
        }

        if (GlobalFeatureManager.Instance.IsEnabled<MediaFeature>())
        {
            builder.Entity<MediaDescriptor>(b =>
            {
                b.ToTable(SmartSoftwareCmsKitDbProperties.DbTablePrefix + "MediaDescriptors", SmartSoftwareCmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.EntityType).IsRequired().HasMaxLength(MediaDescriptorConsts.MaxEntityTypeLength);
                b.Property(x => x.Name).IsRequired().HasMaxLength(MediaDescriptorConsts.MaxNameLength);
                b.Property(x => x.MimeType).IsRequired().HasMaxLength(MediaDescriptorConsts.MaxMimeTypeLength);
                b.Property(x => x.Size).HasMaxLength(MediaDescriptorConsts.MaxSizeLength);

                b.ApplyObjectExtensionMappings();
            });
        }
        else
        {
            builder.Ignore<MediaDescriptor>();
        }

        if (GlobalFeatureManager.Instance.IsEnabled<MenuFeature>())
        {
            builder.Entity<MenuItem>(b =>
            {
                b.ToTable(SmartSoftwareCmsKitDbProperties.DbTablePrefix + "MenuItems", SmartSoftwareCmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.DisplayName).IsRequired().HasMaxLength(MenuItemConsts.MaxDisplayNameLength);

                b.Property(x => x.Url).IsRequired().HasMaxLength(MenuItemConsts.MaxUrlLength);
            });
        }
        else
        {
            builder.Ignore<MenuItem>();
        }

        if (GlobalFeatureManager.Instance.IsEnabled<GlobalResourcesFeature>())
        {
            builder.Entity<GlobalResource>(b =>
            {
                b.ToTable(SmartSoftwareCmsKitDbProperties.DbTablePrefix + "GlobalResources", SmartSoftwareCmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(GlobalResourceConsts.MaxNameLength);

                b.Property(x => x.Value).IsRequired().HasMaxLength(GlobalResourceConsts.MaxValueLength);
            });
        }
        else
        {
            builder.Ignore<GlobalResource>();
        }

        builder.TryConfigureObjectExtensions<CmsKitDbContext>();
    }
}
