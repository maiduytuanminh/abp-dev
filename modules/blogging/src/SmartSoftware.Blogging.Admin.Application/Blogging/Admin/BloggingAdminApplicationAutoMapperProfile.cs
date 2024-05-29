using AutoMapper;
using SmartSoftware.Blogging.Admin.Blogs;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Blogs.Dtos;

namespace SmartSoftware.Blogging.Admin
{
     public class BloggingAdminApplicationAutoMapperProfile : Profile
    {
        public BloggingAdminApplicationAutoMapperProfile()
        {
            CreateMap<Blog, BlogDto>();
        }
    }
}
