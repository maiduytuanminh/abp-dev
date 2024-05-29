using SmartSoftware.Account.Localization;
using SmartSoftware.Emailing.Templates;
using SmartSoftware.Localization;
using SmartSoftware.TextTemplating;

namespace SmartSoftware.Account.Emailing.Templates;

public class AccountEmailTemplateDefinitionProvider : TemplateDefinitionProvider
{
    public override void Define(ITemplateDefinitionContext context)
    {
        context.Add(
            new TemplateDefinition(
                AccountEmailTemplates.PasswordResetLink,
                displayName: LocalizableString.Create<AccountResource>($"TextTemplate:{AccountEmailTemplates.PasswordResetLink}"),
                layout: StandardEmailTemplates.Layout,
                localizationResource: typeof(AccountResource)
            ).WithVirtualFilePath("/SmartSoftware/Account/Emailing/Templates/PasswordResetLink.tpl", true)
        );
    }
}
