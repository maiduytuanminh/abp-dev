using SmartSoftware.Account.Web.Pages.Account;
using SmartSoftware.Identity;
using AutoMapper;
using SmartSoftware.Account.Web.Pages.Account.Components.ProfileManagementGroup.PersonalInfo;

namespace SmartSoftware.Account.Web;

public class SmartSoftwareAccountWebAutoMapperProfile : Profile
{
    public SmartSoftwareAccountWebAutoMapperProfile()
    {
        CreateMap<ProfileDto, AccountProfilePersonalInfoManagementGroupViewComponent.PersonalInfoModel>()
            .MapExtraProperties();
    }
}
