using AutoMapper;
using SmartSoftware.SettingManagement.Web.Pages.SettingManagement.Components.EmailSettingGroup;

namespace SmartSoftware.SettingManagement.Web;

public class SettingManagementWebAutoMapperProfile : Profile
{
    public SettingManagementWebAutoMapperProfile()
    {
        CreateMap<EmailSettingsDto, EmailSettingGroupViewComponent.UpdateEmailSettingsViewModel>();
        
        CreateMap<SendTestEmailModal.SendTestEmailViewModel, SendTestEmailInput>();
    }
}