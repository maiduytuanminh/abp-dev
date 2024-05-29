using AutoMapper;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.Tags;
using SmartSoftware.CmsKit.Users;

namespace SmartSoftware.CmsKit;

public class CmsKitCommonApplicationAutoMapperProfile : Profile
{
    public CmsKitCommonApplicationAutoMapperProfile()
    {
        CreateMap<Tag, TagDto>().MapExtraProperties();

        CreateMap<PopularTag, PopularTagDto>();

        CreateMap<CmsUser, CmsUserDto>().MapExtraProperties();

        CreateMap<BlogFeature, BlogFeatureCacheItem>().MapExtraProperties();
        CreateMap<BlogFeature, BlogFeatureDto>().MapExtraProperties();
        CreateMap<BlogFeatureCacheItem, BlogFeatureDto>()
            .MapExtraProperties()
            .ReverseMap()
            .MapExtraProperties();
    }
}
