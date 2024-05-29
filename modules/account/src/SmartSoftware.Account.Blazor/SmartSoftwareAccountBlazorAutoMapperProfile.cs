using AutoMapper;
using SmartSoftware.Account.Blazor.Pages.Account;
using SmartSoftware.AutoMapper;
using SmartSoftware.Identity;

namespace SmartSoftware.Account.Blazor;

public class SmartSoftwareAccountBlazorAutoMapperProfile : Profile
{
    public SmartSoftwareAccountBlazorAutoMapperProfile()
    {
        CreateMap<ProfileDto, PersonalInfoModel>()
            .MapExtraProperties()
            .Ignore(x => x.PhoneNumberConfirmed)
            .Ignore(x => x.EmailConfirmed);

        CreateMap<PersonalInfoModel, UpdateProfileDto>()
            .MapExtraProperties();
    }
}
