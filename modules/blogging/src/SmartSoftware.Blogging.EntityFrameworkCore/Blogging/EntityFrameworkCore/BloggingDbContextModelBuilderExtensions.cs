using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SmartSoftware;
using SmartSoftware.EntityFrameworkCore.Modeling;
using SmartSoftware.Users.EntityFrameworkCore;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.Posts;
using SmartSoftware.Blogging.Tagging;
using SmartSoftware.Blogging.Users;

namespace SmartSoftware.Blogging.EntityFrameworkCore
{
    public static class BloggingDbContextModelBuilderExtensions
    {
        public static void ConfigureBlogging(
            [NotNull] this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            if (builder.IsTenantOnlyDatabase())
            {
                return;
            }

            builder.Entity<BlogUser>(b =>
            {
                b.ToTable(SmartSoftwareBloggingDbProperties.DbTablePrefix + "Users", SmartSoftwareBloggingDbProperties.DbSchema);
                b.ConfigureByConvention();
                
                b.Property<string>(nameof(BlogUser.Biography)).HasMaxLength(UserConsts.MaxBiographyLength).HasColumnName(nameof(BlogUser.Biography));
                b.Property<string>(nameof(BlogUser.WebSite)).HasMaxLength(UserConsts.MaxWebSiteLength).HasColumnName(nameof(BlogUser.WebSite));
                b.Property<string>(nameof(BlogUser.Twitter)).HasMaxLength(UserConsts.MaxTwitterLength).HasColumnName(nameof(BlogUser.Twitter));
                b.Property<string>(nameof(BlogUser.Github)).HasMaxLength(UserConsts.MaxGithubLength).HasColumnName(nameof(BlogUser.Github));
                b.Property<string>(nameof(BlogUser.Linkedin)).HasMaxLength(UserConsts.MaxLinkedinLength).HasColumnName(nameof(BlogUser.Linkedin));
                b.Property<string>(nameof(BlogUser.Company)).HasMaxLength(UserConsts.MaxCompanyLength).HasColumnName(nameof(BlogUser.Company));
                b.Property<string>(nameof(BlogUser.JobTitle)).HasMaxLength(UserConsts.MaxJobTitleLength).HasColumnName(nameof(BlogUser.JobTitle));
                
                b.ConfigureSmartSoftwareUser();

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<Blog>(b =>
            {
                b.ToTable(SmartSoftwareBloggingDbProperties.DbTablePrefix + "Blogs", SmartSoftwareBloggingDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(BlogConsts.MaxNameLength).HasColumnName(nameof(Blog.Name));
                b.Property(x => x.ShortName).IsRequired().HasMaxLength(BlogConsts.MaxShortNameLength).HasColumnName(nameof(Blog.ShortName));
                b.Property(x => x.Description).IsRequired(false).HasMaxLength(BlogConsts.MaxDescriptionLength).HasColumnName(nameof(Blog.Description));

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<Post>(b =>
            {
                b.ToTable(SmartSoftwareBloggingDbProperties.DbTablePrefix + "Posts", SmartSoftwareBloggingDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.BlogId).HasColumnName(nameof(Post.BlogId));
                b.Property(x => x.Title).IsRequired().HasMaxLength(PostConsts.MaxTitleLength).HasColumnName(nameof(Post.Title));
                b.Property(x => x.CoverImage).IsRequired().HasColumnName(nameof(Post.CoverImage));
                b.Property(x => x.Url).IsRequired().HasMaxLength(PostConsts.MaxUrlLength).HasColumnName(nameof(Post.Url));
                b.Property(x => x.Content).IsRequired(false).HasMaxLength(PostConsts.MaxContentLength).HasColumnName(nameof(Post.Content));
                b.Property(x => x.Description).IsRequired(false).HasMaxLength(PostConsts.MaxDescriptionLength).HasColumnName(nameof(Post.Description));

                b.HasMany(p => p.Tags).WithOne().HasForeignKey(qt => qt.PostId);

                b.HasOne<Blog>().WithMany().IsRequired().HasForeignKey(p => p.BlogId);

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<Comment>(b =>
            {
                b.ToTable(SmartSoftwareBloggingDbProperties.DbTablePrefix + "Comments", SmartSoftwareBloggingDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Text).IsRequired().HasMaxLength(CommentConsts.MaxTextLength).HasColumnName(nameof(Comment.Text));
                b.Property(x => x.RepliedCommentId).HasColumnName(nameof(Comment.RepliedCommentId));
                b.Property(x => x.PostId).IsRequired().HasColumnName(nameof(Comment.PostId));

                b.HasOne<Comment>().WithMany().HasForeignKey(p => p.RepliedCommentId);
                b.HasOne<Post>().WithMany().IsRequired().HasForeignKey(p => p.PostId);

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<Tag>(b =>
            {
                b.ToTable(SmartSoftwareBloggingDbProperties.DbTablePrefix + "Tags", SmartSoftwareBloggingDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(TagConsts.MaxNameLength).HasColumnName(nameof(Tag.Name));
                b.Property(x => x.Description).HasMaxLength(TagConsts.MaxDescriptionLength).HasColumnName(nameof(Tag.Description));
                b.Property(x => x.UsageCount).HasColumnName(nameof(Tag.UsageCount));

                b.HasMany<PostTag>().WithOne().HasForeignKey(qt => qt.TagId);

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<PostTag>(b =>
            {
                b.ToTable(SmartSoftwareBloggingDbProperties.DbTablePrefix + "PostTags", SmartSoftwareBloggingDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.PostId).HasColumnName(nameof(PostTag.PostId));
                b.Property(x => x.TagId).HasColumnName(nameof(PostTag.TagId));

                b.HasKey(x => new { x.PostId, x.TagId });

                b.ApplyObjectExtensionMappings();
            });

            builder.TryConfigureObjectExtensions<BloggingDbContext>();
        }
    }
}
