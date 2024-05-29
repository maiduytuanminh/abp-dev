using NUglify;
using SmartSoftware.Minify.Scripts;

namespace SmartSoftware.Minify.NUglify;

public class NUglifyJavascriptMinifier : NUglifyMinifierBase, IJavascriptMinifier
{
    protected override UglifyResult UglifySource(string source, string? fileName)
    {
        return Uglify.Js(source, fileName);
    }
}
