using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.VirtualFileSystem;
using SmartSoftware.Minify.Styles;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.Styles;

public class StyleBundler : BundlerBase, IStyleBundler
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    public override string FileExtension => "css";

    public StyleBundler(
        IWebHostEnvironment hostEnvironment,
        ICssMinifier minifier,
        IOptions<SmartSoftwareBundlingOptions> bundlingOptions)
        : base(
            hostEnvironment,
            minifier,
            bundlingOptions)
    {
        _hostingEnvironment = hostEnvironment;
    }

    public string GetAbsolutePath(string relativePath)
    {
        return Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", relativePath.RemovePreFix("/"));
    }

    protected override string ProcessBeforeAddingToTheBundle(IBundlerContext context, string filePath, string fileContent)
    {
        return CssRelativePath.Adjust(
            fileContent,
            GetAbsolutePath(filePath),
            GetAbsolutePath(context.BundleRelativePath)
        );
    }
}
