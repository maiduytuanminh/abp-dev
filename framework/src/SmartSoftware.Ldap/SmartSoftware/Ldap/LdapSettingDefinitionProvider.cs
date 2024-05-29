using SmartSoftware.Ldap.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Settings;

namespace SmartSoftware.Ldap;

public class LdapSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(
                LdapSettingNames.Ldaps,
                "false",
                L("DisplayName:SmartSoftware.Ldap.Ldaps"),
                L("Description:SmartSoftware.Ldap.Ldaps")),

            new SettingDefinition(
                LdapSettingNames.ServerHost,
                "",
                L("DisplayName:SmartSoftware.Ldap.ServerHost"),
                L("Description:SmartSoftware.Ldap.ServerHost")),

            new SettingDefinition(
                LdapSettingNames.ServerPort,
                "389",
                L("DisplayName:SmartSoftware.Ldap.ServerPort"),
                L("Description:SmartSoftware.Ldap.ServerPort")),

            new SettingDefinition(
                LdapSettingNames.BaseDc,
                "",
                L("DisplayName:SmartSoftware.Ldap.BaseDc"),
                L("Description:SmartSoftware.Ldap.BaseDc")),

            new SettingDefinition(
                LdapSettingNames.Domain,
                "",
                L("DisplayName:SmartSoftware.Ldap.Domain"),
                L("Description:SmartSoftware.Ldap.Domain")),

            new SettingDefinition(
                LdapSettingNames.UserName,
                "",
                L("DisplayName:SmartSoftware.Ldap.UserName"),
                L("Description:SmartSoftware.Ldap.UserName")),

            new SettingDefinition(
                LdapSettingNames.Password,
                "",
                L("DisplayName:SmartSoftware.Ldap.Password"),
                L("Description:SmartSoftware.Ldap.Password"),
                isEncrypted: true)
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LdapResource>(name);
    }
}
