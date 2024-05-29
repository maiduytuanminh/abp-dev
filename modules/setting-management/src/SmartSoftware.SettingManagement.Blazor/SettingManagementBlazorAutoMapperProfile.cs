using AutoMapper;
using SmartSoftware.SettingManagement.Blazor.Pages.SettingManagement.EmailSettingGroup;

namespace SmartSoftware.SettingManagement.Blazor;

public class SettingManagementBlazorAutoMapperProfile : Profile
{
    public SettingManagementBlazorAutoMapperProfile()
    {
        CreateMap<EmailSettingGroupViewComponent.UpdateEmailSettingsViewModel, UpdateEmailSettingsDto>();
        CreateMap<EmailSettingsDto, EmailSettingGroupViewComponent.UpdateEmailSettingsViewModel>();

        CreateMap<EmailSettingGroupViewComponent.SendTestEmailViewModel, SendTestEmailInput>();
    }
}
