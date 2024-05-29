using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.TextTemplating.VirtualFiles;
using Xunit;

namespace SmartSoftware.TextTemplating.Scriban.SampleTemplates;

public class ScribanTemplateContentFileProvider_Tests : TemplateContentFileProvider_Tests<ScribanTextTemplatingTestModule>
{
    [Fact]
    public async Task GetScribanFilesAsync()
    {
        var definition = await TemplateDefinitionManager.GetAsync(TestTemplates.WelcomeEmail);
        var files = await TemplateContentFileProvider.GetFilesAsync(definition);
        files.Count.ShouldBe(2);
        files.ShouldContain(x => x.FileName == "en.tpl"  && x.FileContent.Contains("Welcome {{model.name}} to the smartsoftware.io!"));
        files.ShouldContain(x => x.FileName == "tr.tpl"  && x.FileContent.Contains("Merhaba {{model.name}}, smartsoftware.io'ya hoşgeldiniz!"));
    }
}
