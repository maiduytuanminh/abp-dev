using SmartSoftware.Localization;
using SmartSoftware.Settings;
using SmartSoftware.Timing.Localization.Resources.SmartSoftwareTiming;

namespace SmartSoftware.Timing;

public class TimingSettingProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(TimingSettingNames.TimeZone,
                "UTC",
                L("DisplayName:SmartSoftware.Timing.Timezone"),
                L("Description:SmartSoftware.Timing.Timezone"),
                isVisibleToClients: true)
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SmartSoftwareTimingResource>(name);
    }
}
