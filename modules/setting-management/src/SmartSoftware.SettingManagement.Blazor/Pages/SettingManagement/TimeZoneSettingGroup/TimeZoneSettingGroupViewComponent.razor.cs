using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SmartSoftware.AspNetCore.Components.Messages;
using SmartSoftware.AspNetCore.Components.Web.Configuration;
using SmartSoftware.SettingManagement.Localization;

namespace SmartSoftware.SettingManagement.Blazor.Pages.SettingManagement.TimeZoneSettingGroup;

public partial class TimeZoneSettingGroupViewComponent
{
    [Inject]
    protected ITimeZoneSettingsAppService TimeZoneSettingsAppService { get; set; }

    [Inject]
    private ICurrentApplicationConfigurationCacheResetService CurrentApplicationConfigurationCacheResetService { get; set; }

    [Inject]
    protected IUiMessageService UiMessageService { get; set; }

    protected UpdateTimezoneSettingsViewModel TimezoneSettings;

    public TimeZoneSettingGroupViewComponent()
    {
        ObjectMapperContext = typeof(SmartSoftwareSettingManagementBlazorModule);
        LocalizationResource = typeof(SmartSoftwareSettingManagementResource);
    }

    protected async override Task OnInitializedAsync()
    {
        TimezoneSettings = new UpdateTimezoneSettingsViewModel()
        {
            Timezone = await TimeZoneSettingsAppService.GetAsync(),
            TimeZoneItems = await TimeZoneSettingsAppService.GetTimezonesAsync()
        };
    }

    protected virtual async Task OnSelectedValueChangedAsync(string timezone)
    {
        TimezoneSettings.Timezone = timezone;
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task UpdateSettingsAsync()
    {
        try
        {
            await TimeZoneSettingsAppService.UpdateAsync(TimezoneSettings.Timezone);
            await CurrentApplicationConfigurationCacheResetService.ResetAsync();
            await Notify.Success(L["SavedSuccessfully"]);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    public class UpdateTimezoneSettingsViewModel
    {
        public string Timezone { get; set; }

        public List<NameValue> TimeZoneItems { get; set; }
    }
}
