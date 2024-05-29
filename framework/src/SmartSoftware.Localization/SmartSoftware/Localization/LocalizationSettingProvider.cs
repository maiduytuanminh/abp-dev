using SmartSoftware.Localization.Resources.SmartSoftwareLocalization;
using SmartSoftware.Settings;

namespace SmartSoftware.Localization;

public class LocalizationSettingProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(LocalizationSettingNames.DefaultLanguage,
                "en",
                L("DisplayName:SmartSoftware.Localization.DefaultLanguage"),
                L("Description:SmartSoftware.Localization.DefaultLanguage"),
                isVisibleToClients: true)
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SmartSoftwareLocalizationResource>(name);
    }
}
