using Shouldly;
using SmartSoftware.Minify.Html;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Minify.NUglify;

public class HtmlMinifier_Tests : SmartSoftwareIntegratedTest<SmartSoftwareMinifyModule>
{
    private readonly IHtmlMinifier _htmlMinifier;

    public HtmlMinifier_Tests()
    {
        _htmlMinifier = GetRequiredService<IHtmlMinifier>();
    }

    [Fact]
    public void Should_Minify_Simple_Code()
    {
        const string source = "<div>  <p>This is <em>   a text    </em></p>   </div>";

        var minified = _htmlMinifier.Minify(source);

        minified.Length.ShouldBeGreaterThan(0);
        minified.Length.ShouldBeLessThan(source.Length);
    }
}
