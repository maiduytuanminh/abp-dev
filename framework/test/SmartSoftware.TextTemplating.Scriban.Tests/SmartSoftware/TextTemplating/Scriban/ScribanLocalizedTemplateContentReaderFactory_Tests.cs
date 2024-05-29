using System.IO;
using Microsoft.Extensions.FileProviders;
using SmartSoftware.TextTemplating.VirtualFiles;

namespace SmartSoftware.TextTemplating.Scriban;

public class ScribanLocalizedTemplateContentReaderFactory_Tests : LocalizedTemplateContentReaderFactory_Tests<ScribanTextTemplatingTestModule>
{
    public ScribanLocalizedTemplateContentReaderFactory_Tests()
    {
        LocalizedTemplateContentReaderFactory = new LocalizedTemplateContentReaderFactory(
            new PhysicalFileVirtualFileProvider(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),
                    "SmartSoftware", "SmartSoftware", "TextTemplating", "Scriban"))));

        WelcomeEmailEnglishContent = "Welcome {{model.name}} to the smartsoftware.io!";
        WelcomeEmailTurkishContent = "Merhaba {{model.name}}, smartsoftware.io'ya hoşgeldiniz!";
    }
}
