using AutoMapper;
using SmartSoftware.AutoMapper;
using SmartSoftware.CmsKit.Menus;
using SmartSoftware.CmsKit.Public.Comments;

namespace SmartSoftware.CmsKit.Public.Web;

public class CmsKitPublicWebAutoMapperProfile : Profile
{
    public CmsKitPublicWebAutoMapperProfile()
    {
        CreateMap<CreateCommentWithParametersInput, CreateCommentInput>()
            .Ignore(x=> x.ExtraProperties);
    }
}
