using SmartSoftware.Emailing.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Settings;

namespace SmartSoftware.Emailing;

/// <summary>
/// Defines settings to send emails.
/// <see cref="EmailSettingNames"/> for all available configurations.
/// </summary>
internal class EmailSettingProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(
                EmailSettingNames.Smtp.Host,
                "127.0.0.1",
                L("DisplayName:SmartSoftware.Mailing.Smtp.Host"),
                L("Description:SmartSoftware.Mailing.Smtp.Host")),

            new SettingDefinition(EmailSettingNames.Smtp.Port,
                "25",
                L("DisplayName:SmartSoftware.Mailing.Smtp.Port"),
                L("Description:SmartSoftware.Mailing.Smtp.Port")),

            new SettingDefinition(
                EmailSettingNames.Smtp.UserName,
                displayName: L("DisplayName:SmartSoftware.Mailing.Smtp.UserName"),
                description: L("Description:SmartSoftware.Mailing.Smtp.UserName")),

            new SettingDefinition(
                EmailSettingNames.Smtp.Password,
                displayName:
                L("DisplayName:SmartSoftware.Mailing.Smtp.Password"),
                description: L("Description:SmartSoftware.Mailing.Smtp.Password"),
                isEncrypted: true),

            new SettingDefinition(
                EmailSettingNames.Smtp.Domain,
                displayName: L("DisplayName:SmartSoftware.Mailing.Smtp.Domain"),
                description: L("Description:SmartSoftware.Mailing.Smtp.Domain")),

            new SettingDefinition(
                EmailSettingNames.Smtp.EnableSsl,
                "false",
                L("DisplayName:SmartSoftware.Mailing.Smtp.EnableSsl"),
                L("Description:SmartSoftware.Mailing.Smtp.EnableSsl")),

            new SettingDefinition(
                EmailSettingNames.Smtp.UseDefaultCredentials,
                "true",
                L("DisplayName:SmartSoftware.Mailing.Smtp.UseDefaultCredentials"),
                L("Description:SmartSoftware.Mailing.Smtp.UseDefaultCredentials")),

            new SettingDefinition(
                EmailSettingNames.DefaultFromAddress,
                "noreply@smartsoftware.io",
                L("DisplayName:SmartSoftware.Mailing.DefaultFromAddress"),
                L("Description:SmartSoftware.Mailing.DefaultFromAddress")),

            new SettingDefinition(EmailSettingNames.DefaultFromDisplayName,
                "SS application",
                L("DisplayName:SmartSoftware.Mailing.DefaultFromDisplayName"),
                L("Description:SmartSoftware.Mailing.DefaultFromDisplayName"))
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EmailingResource>(name);
    }
}
