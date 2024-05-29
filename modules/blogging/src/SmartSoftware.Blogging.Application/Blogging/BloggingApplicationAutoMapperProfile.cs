using AutoMapper;
using SmartSoftware.AutoMapper;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Blogs.Dtos;
using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.Comments.Dtos;
using SmartSoftware.Blogging.Posts;
using SmartSoftware.Blogging.Tagging;
using SmartSoftware.Blogging.Tagging.Dtos;
using SmartSoftware.Blogging.Users;

namespace SmartSoftware.Blogging
{
    public class BloggingApplicationAutoMapperProfile : Profile
    {
        public BloggingApplicationAutoMapperProfile()
        {
            CreateMap<Blog, BlogDto>();
            CreateMap<BlogUser, BlogUserDto>();
            CreateMap<Post, PostWithDetailsDto>().Ignore(x=>x.Writer).Ignore(x=>x.CommentCount).Ignore(x=>x.Tags);
            CreateMap<Comment, CommentWithDetailsDto>().Ignore(x => x.Writer);
            CreateMap<Tag, TagDto>();
            CreateMap<Post, PostCacheItem>().Ignore(x=>x.CommentCount).Ignore(x=>x.Tags);
            CreateMap<PostCacheItem, PostWithDetailsDto>()
                .IgnoreModificationAuditedObjectProperties()
                .IgnoreDeletionAuditedObjectProperties()
                .Ignore(x => x.ConcurrencyStamp)
                .Ignore(x => x.Writer)
                .Ignore(x => x.CommentCount)
                .Ignore(x => x.Tags);
        }
    }
}
