using System.Collections.Generic;

namespace SmartSoftware.SettingManagement.Blazor;

public class SettingManagementComponentOptions
{
    public List<ISettingComponentContributor> Contributors { get; }

    public SettingManagementComponentOptions()
    {
        Contributors = new List<ISettingComponentContributor>();
    }
}
