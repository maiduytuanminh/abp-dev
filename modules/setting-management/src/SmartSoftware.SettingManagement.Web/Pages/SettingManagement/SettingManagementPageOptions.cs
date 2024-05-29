using System.Collections.Generic;

namespace SmartSoftware.SettingManagement.Web.Pages.SettingManagement;

public class SettingManagementPageOptions
{
    public List<ISettingPageContributor> Contributors { get; }

    public SettingManagementPageOptions()
    {
        Contributors = new List<ISettingPageContributor>();
    }
}
