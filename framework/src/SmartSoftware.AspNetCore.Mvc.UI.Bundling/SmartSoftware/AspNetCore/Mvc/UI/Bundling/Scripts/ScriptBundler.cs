using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.VirtualFileSystem;
using SmartSoftware.Minify.Scripts;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.Scripts;

public class ScriptBundler : BundlerBase, IScriptBundler
{
    public override string FileExtension => "js";

    public ScriptBundler(
        IWebHostEnvironment hostEnvironment,
        IJavascriptMinifier minifier,
        IOptions<SmartSoftwareBundlingOptions> bundlingOptions)
        : base(
            hostEnvironment,
            minifier,
            bundlingOptions)
    {
    }

    protected override string ProcessBeforeAddingToTheBundle(IBundlerContext context, string filePath, string fileContent)
    {
        return fileContent.EnsureEndsWith(';') + Environment.NewLine;
    }
}
