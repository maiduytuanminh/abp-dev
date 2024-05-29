using System;
using System.IO;
using Microsoft.Extensions.FileProviders;
using SmartSoftware.TextTemplating.VirtualFiles;

namespace SmartSoftware.TextTemplating.Razor;

public class RazorLocalizedTemplateContentReaderFactory_Tests : LocalizedTemplateContentReaderFactory_Tests<RazorTextTemplatingTestModule>
{
    public RazorLocalizedTemplateContentReaderFactory_Tests()
    {
        LocalizedTemplateContentReaderFactory = new LocalizedTemplateContentReaderFactory(
            new PhysicalFileVirtualFileProvider(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),
                    "SmartSoftware", "TextTemplating", "Razor"))));

        WelcomeEmailEnglishContent = "@inherits SmartSoftware.TextTemplating.Razor.RazorTemplatePageBase<SmartSoftware.TextTemplating.Razor.RazorTemplateRendererProvider_Tests.WelcomeEmailModel>\n" +
                         "Welcome @Model.Name to the smartsoftware.io!";

        WelcomeEmailTurkishContent = "@inherits SmartSoftware.TextTemplating.Razor.RazorTemplatePageBase<SmartSoftware.TextTemplating.Razor.RazorTemplateRendererProvider_Tests.WelcomeEmailModel>\n" +
                         "Merhaba @Model.Name, smartsoftware.io'ya hoşgeldiniz!";
    }
}
