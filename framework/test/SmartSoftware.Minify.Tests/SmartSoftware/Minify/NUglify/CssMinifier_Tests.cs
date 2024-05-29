using Shouldly;
using SmartSoftware.Minify.Styles;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Minify.NUglify;

public class CssMinifier_Tests : SmartSoftwareIntegratedTest<SmartSoftwareMinifyModule>
{
    private readonly ICssMinifier _cssMinifier;

    public CssMinifier_Tests()
    {
        _cssMinifier = GetRequiredService<ICssMinifier>();
    }

    [Fact]
    public void Should_Minify_Simple_Code()
    {
        const string source = "div { color: #FFF; }";

        var minified = _cssMinifier.Minify(source);

        minified.Length.ShouldBeGreaterThan(0);
        minified.Length.ShouldBeLessThan(source.Length);
    }
}
