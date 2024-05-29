using AutoMapper;
using SmartSoftware.Blogging.Admin.Blogs;
using SmartSoftware.Blogging.Admin.Pages.Blogging.Admin.Blogs;
using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.Blogs.Dtos;
using EditModel = SmartSoftware.Blogging.Admin.Pages.Blogging.Admin.Blogs.EditModel;

namespace SmartSoftware.Blogging.Admin
{
    public class SmartSoftwareBloggingAdminWebAutoMapperProfile : Profile
    {
        public SmartSoftwareBloggingAdminWebAutoMapperProfile()
        {
            CreateMap<CreateModel.BlogCreateModalView, CreateBlogDto>();
            CreateMap<BlogDto, EditModel.BlogEditViewModel>();
        }
    }
}
