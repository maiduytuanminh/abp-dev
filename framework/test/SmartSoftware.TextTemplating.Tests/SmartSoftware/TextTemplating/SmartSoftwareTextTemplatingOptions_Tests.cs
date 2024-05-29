using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace SmartSoftware.TextTemplating;

public class SmartSoftwareTextTemplatingOptions_Tests : SmartSoftwareTextTemplatingTestBase<SmartSoftwareTextTemplatingTestModule>
{
    private readonly SmartSoftwareTextTemplatingOptions _options;

    public SmartSoftwareTextTemplatingOptions_Tests()
    {
        _options = GetRequiredService<IOptions<SmartSoftwareTextTemplatingOptions>>().Value;
    }

    [Fact]
    public void Should_Auto_Add_TemplateDefinitionProviders_To_Options()
    {
        _options
            .DefinitionProviders
            .ShouldContain(typeof(TestTemplateDefinitionProvider));
    }
}
