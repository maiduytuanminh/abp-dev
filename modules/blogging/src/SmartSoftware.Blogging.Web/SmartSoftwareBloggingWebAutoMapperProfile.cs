using AutoMapper;
using SmartSoftware.AutoMapper;
using SmartSoftware.Blogging.Pages.Blogs.Posts;
using SmartSoftware.Blogging.Posts;

namespace SmartSoftware.Blogging
{
    public class SmartSoftwareBloggingWebAutoMapperProfile : Profile
    {
        public SmartSoftwareBloggingWebAutoMapperProfile()
        {
            CreateMap<PostWithDetailsDto, EditPostViewModel>().Ignore(x=>x.Tags);
            CreateMap<NewModel.CreatePostViewModel, CreatePostDto>();
        }
    }
}
