using AutoMapper;
using SmartSoftware.AutoMapper;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.Comments;
using SmartSoftware.CmsKit.Contents;
using SmartSoftware.CmsKit.GlobalResources;
using SmartSoftware.CmsKit.Menus;
using SmartSoftware.CmsKit.Pages;
using SmartSoftware.CmsKit.Public.Blogs;
using SmartSoftware.CmsKit.Public.Comments;
using SmartSoftware.CmsKit.Public.GlobalResources;
using SmartSoftware.CmsKit.Public.Ratings;
using SmartSoftware.CmsKit.Ratings;
using SmartSoftware.CmsKit.Users;

namespace SmartSoftware.CmsKit.Public;

public class PublicApplicationAutoMapperProfile : Profile
{
    public PublicApplicationAutoMapperProfile()
    {
        CreateMap<CmsUser, Comments.CmsUserDto>().MapExtraProperties();

        CreateMap<Comment, CommentDto>()
            .Ignore(x => x.Author).MapExtraProperties();

        CreateMap<Comment, CommentWithDetailsDto>()
            .Ignore(x => x.Replies)
            .Ignore(x => x.Author)
            .MapExtraProperties();

        CreateMap<Rating, RatingDto>();

        CreateMap<Page, PageCacheItem>().MapExtraProperties();

        CreateMap<PageCacheItem, PageDto>().MapExtraProperties();

        CreateMap<Page, PageDto>().MapExtraProperties();
        
        CreateMap<BlogPost, BlogPostCommonDto>().MapExtraProperties();

        CreateMap<MenuItem, MenuItemDto>().MapExtraProperties();

        CreateMap<GlobalResource, GlobalResourceDto>().MapExtraProperties();
    }
}
