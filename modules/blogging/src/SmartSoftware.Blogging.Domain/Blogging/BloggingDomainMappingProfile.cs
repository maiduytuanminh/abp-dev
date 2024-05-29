using AutoMapper;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.Posts;
using SmartSoftware.Blogging.Tagging;

namespace SmartSoftware.Blogging
{
    public class BloggingDomainMappingProfile : Profile
    {
        public BloggingDomainMappingProfile()
        {
            CreateMap<Blog, BlogEto>();
            CreateMap<Comment, CommentEto>();
            CreateMap<Post, PostEto>();
            CreateMap<Tag, TagEto>();
        }
    }
}