using NUglify;
using SmartSoftware.Minify.Styles;

namespace SmartSoftware.Minify.NUglify;

public class NUglifyCssMinifier : NUglifyMinifierBase, ICssMinifier
{
    protected override UglifyResult UglifySource(string source, string? fileName)
    {
        return Uglify.Css(source, fileName);
    }
}
