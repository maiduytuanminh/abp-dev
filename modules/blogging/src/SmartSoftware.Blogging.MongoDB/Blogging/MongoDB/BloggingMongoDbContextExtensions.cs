using SmartSoftware;
using SmartSoftware.MongoDB;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.Posts;
using SmartSoftware.Blogging.Users;

namespace SmartSoftware.Blogging.MongoDB
{
    public static class BloggingMongoDbContextExtensions
    {
        public static void ConfigureBlogging(
            this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<BlogUser>(b =>
            {
                b.CollectionName = SmartSoftwareBloggingDbProperties.DbTablePrefix + "Users";
            });

            builder.Entity<Blog>(b =>
            {
                b.CollectionName = SmartSoftwareBloggingDbProperties.DbTablePrefix + "Blogs";
            });

            builder.Entity<Post>(b =>
            {
                b.CollectionName = SmartSoftwareBloggingDbProperties.DbTablePrefix + "Posts";
            });

            builder.Entity<Tagging.Tag>(b =>
            {
                b.CollectionName = SmartSoftwareBloggingDbProperties.DbTablePrefix + "Tags";
            });

            builder.Entity<Comment>(b =>
            {
                b.CollectionName = SmartSoftwareBloggingDbProperties.DbTablePrefix + "Comments";
            });
        }
    }
}
