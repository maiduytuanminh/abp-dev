using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Auditing;
using SmartSoftware.Timing.Localization.Resources.SmartSoftwareTiming;

namespace SmartSoftware.SettingManagement.Web.Pages.SettingManagement.Components.TimeZoneSettingGroup;

public class TimeZoneSettingGroupViewComponent : SmartSoftwareViewComponent
{
    protected ITimeZoneSettingsAppService TimeZoneSettingsAppService { get; }

    public TimeZoneSettingGroupViewComponent(ITimeZoneSettingsAppService timeZoneSettingsAppService)
    {
        ObjectMapperContext = typeof(SmartSoftwareSettingManagementWebModule);
        TimeZoneSettingsAppService = timeZoneSettingsAppService;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var timezone = await TimeZoneSettingsAppService.GetAsync();
        var timezones = await TimeZoneSettingsAppService.GetTimezonesAsync();
        var model = new UpdateTimezoneSettingsViewModel()
        {
            Timezone = timezone,
            TimeZoneItems = new List<SelectListItem>()
        };
        model.TimeZoneItems.AddRange(timezones.Select(x => new SelectListItem(x.Name, x.Value)).ToList());
        return View("~/Pages/SettingManagement/Components/TimeZoneSettingGroup/Default.cshtml", model);
    }

    public class UpdateTimezoneSettingsViewModel
    {
        public string Timezone { get; set; }

        public List<SelectListItem> TimeZoneItems { get; set; }
    }
}
