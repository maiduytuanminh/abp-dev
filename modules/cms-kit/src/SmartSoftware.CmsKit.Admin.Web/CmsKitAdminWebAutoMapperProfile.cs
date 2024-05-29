using AutoMapper;
using SmartSoftware.AutoMapper;
using SmartSoftware.CmsKit.Admin.Blogs;
using SmartSoftware.CmsKit.Admin.Menus;
using SmartSoftware.CmsKit.Admin.Pages;
using SmartSoftware.CmsKit.Admin.Tags;
using SmartSoftware.CmsKit.Menus;
using SmartSoftware.CmsKit.Tags;

namespace SmartSoftware.CmsKit.Admin.Web;

public class CmsKitAdminWebAutoMapperProfile : Profile
{
    public CmsKitAdminWebAutoMapperProfile()
    {
        CreateBlogPostMappings();
        CreateBlogMappings();
        CreateMenuMappings();
        CreatePageMappings();
        CreateTagMappings();
    }

    protected virtual void CreateBlogPostMappings()
    {
        CreateMap<SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.BlogPosts.CreateModel.CreateBlogPostViewModel, CreateBlogPostDto>().MapExtraProperties();
        CreateMap<SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.BlogPosts.UpdateModel.UpdateBlogPostViewModel, UpdateBlogPostDto>().MapExtraProperties();
        CreateMap<BlogPostDto, SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.BlogPosts.UpdateModel.UpdateBlogPostViewModel>().MapExtraProperties();
    }
    
    protected virtual void CreateBlogMappings()
    {
        CreateMap<SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Blogs.CreateModalModel.CreateBlogViewModel, CreateBlogDto>().MapExtraProperties();
        CreateMap<SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Blogs.UpdateModalModel.UpdateBlogViewModel, UpdateBlogDto>().MapExtraProperties();
        CreateMap<BlogDto, SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Blogs.UpdateModalModel.UpdateBlogViewModel>().MapExtraProperties();
    }

    protected virtual void CreateMenuMappings()
    {
        CreateMap<SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Menus.MenuItems.CreateModalModel.MenuItemCreateViewModel, MenuItemCreateInput>().MapExtraProperties();
        CreateMap<SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Menus.MenuItems.UpdateModalModel.MenuItemUpdateViewModel, MenuItemUpdateInput>().MapExtraProperties();
        CreateMap<MenuItemWithDetailsDto, SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Menus.MenuItems.UpdateModalModel.MenuItemUpdateViewModel>().MapExtraProperties();
    }

    protected virtual void CreatePageMappings()
    {
        CreateMap<SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Pages.CreateModel.CreatePageViewModel, CreatePageInputDto>().MapExtraProperties();
        CreateMap<SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Pages.UpdateModel.UpdatePageViewModel, UpdatePageInputDto>().MapExtraProperties();
        CreateMap<PageDto, SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Pages.UpdateModel.UpdatePageViewModel>().MapExtraProperties();
    }

    protected virtual void CreateTagMappings()
    {
        CreateMap<SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Tags.CreateModalModel.TagCreateViewModel, TagCreateDto>().MapExtraProperties();
        CreateMap<SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Tags.EditModalModel.TagEditViewModel, TagUpdateDto>().MapExtraProperties();
        CreateMap<TagDto, SmartSoftware.CmsKit.Admin.Web.Pages.CmsKit.Tags.EditModalModel.TagEditViewModel>().MapExtraProperties();
    }
}