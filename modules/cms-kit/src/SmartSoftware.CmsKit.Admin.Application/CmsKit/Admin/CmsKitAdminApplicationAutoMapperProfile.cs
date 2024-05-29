using AutoMapper;
using SmartSoftware.AutoMapper;
using SmartSoftware.CmsKit.Admin.Blogs;
using SmartSoftware.CmsKit.Admin.Comments;
using SmartSoftware.CmsKit.Admin.MediaDescriptors;
using SmartSoftware.CmsKit.Admin.Pages;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.Admin.Tags;
using SmartSoftware.CmsKit.Comments;
using SmartSoftware.CmsKit.MediaDescriptors;
using SmartSoftware.CmsKit.Pages;
using SmartSoftware.CmsKit.Tags;
using SmartSoftware.CmsKit.Users;
using SmartSoftware.CmsKit.Menus;
using SmartSoftware.CmsKit.Admin.Menus;

namespace SmartSoftware.CmsKit.Admin;

public class CmsKitAdminApplicationAutoMapperProfile : Profile
{
    public CmsKitAdminApplicationAutoMapperProfile()
    {
        CreateMap<CmsUser, Comments.CmsUserDto>().MapExtraProperties();

        CreateMap<Comment, CommentDto>().MapExtraProperties();
        CreateMap<Comment, CommentWithAuthorDto>()
            .Ignore(x => x.Author)
            .MapExtraProperties();

        CreateMap<Page, PageDto>().MapExtraProperties();
        CreateMap<Page, PageLookupDto>();

        CreateMap<BlogPost, BlogPostDto>(MemberList.Destination).MapExtraProperties();
        CreateMap<BlogPost, BlogPostListDto>()
            .Ignore(d => d.BlogName)
            .MapExtraProperties();
        CreateMap<CreateBlogPostDto, BlogPost>(MemberList.Source).MapExtraProperties();
        CreateMap<UpdateBlogPostDto, BlogPost>(MemberList.Source).MapExtraProperties();

        CreateMap<Blog, BlogDto>().MapExtraProperties();

        CreateMap<TagEntityTypeDefiniton, TagDefinitionDto>(MemberList.Destination);

        CreateMap<Tag, TagDto>().MapExtraProperties();

        CreateMap<MediaDescriptor, MediaDescriptorDto>().MapExtraProperties();

        CreateMap<MenuItem, MenuItemDto>().MapExtraProperties();
        CreateMap<MenuItem, MenuItemWithDetailsDto>()
            .Ignore(x => x.PageTitle)
            .MapExtraProperties();
    }
}
