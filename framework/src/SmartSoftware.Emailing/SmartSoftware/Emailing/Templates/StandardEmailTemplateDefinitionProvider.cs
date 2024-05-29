using SmartSoftware.Emailing.Localization;
using SmartSoftware.Localization;
using SmartSoftware.TextTemplating;

namespace SmartSoftware.Emailing.Templates;

public class StandardEmailTemplateDefinitionProvider : TemplateDefinitionProvider
{
    public override void Define(ITemplateDefinitionContext context)
    {
        context.Add(
            new TemplateDefinition(
                StandardEmailTemplates.Layout,
                displayName: LocalizableString.Create<EmailingResource>("TextTemplate:StandardEmailTemplates.Layout"),
                isLayout: true
            ).WithVirtualFilePath("/SmartSoftware/Emailing/Templates/Layout.tpl", true)
        );

        context.Add(
            new TemplateDefinition(
                StandardEmailTemplates.Message,
                displayName: LocalizableString.Create<EmailingResource>("TextTemplate:StandardEmailTemplates.Message"),
                layout: StandardEmailTemplates.Layout
            ).WithVirtualFilePath("/SmartSoftware/Emailing/Templates/Message.tpl", true)
        );
    }
}
