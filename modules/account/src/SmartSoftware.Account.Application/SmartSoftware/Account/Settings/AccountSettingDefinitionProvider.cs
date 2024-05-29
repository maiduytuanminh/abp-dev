using SmartSoftware.Account.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Settings;

namespace SmartSoftware.Account.Settings;

public class AccountSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(
                AccountSettingNames.IsSelfRegistrationEnabled,
                "true",
                L("DisplayName:SmartSoftware.Account.IsSelfRegistrationEnabled"),
                L("Description:SmartSoftware.Account.IsSelfRegistrationEnabled"), isVisibleToClients: true)
        );

        context.Add(
            new SettingDefinition(
                AccountSettingNames.EnableLocalLogin,
                "true",
                L("DisplayName:SmartSoftware.Account.EnableLocalLogin"),
                L("Description:SmartSoftware.Account.EnableLocalLogin"), isVisibleToClients: true)
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AccountResource>(name);
    }
}
