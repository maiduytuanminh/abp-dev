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
    public class BloggingMongoDbContext : SmartSoftwareMongoDbContext, IBloggingMongoDbContext
    {
        public IMongoCollection<BlogUser> Users => Collection<BlogUser>();

        public IMongoCollection<Blog> Blogs => Collection<Blog>();

        public IMongoCollection<Post> Posts => Collection<Post>();

        public IMongoCollection<Tagging.Tag> Tags => Collection<Tagging.Tag>();

        public IMongoCollection<Comment> Comments => Collection<Comment>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureBlogging();
        }
    }
}
