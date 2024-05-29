﻿using JetBrains.Annotations;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public abstract class SmartSoftwareTagHelperResourceService : ITransientDependency
{
    public ILogger<SmartSoftwareTagHelperResourceService> Logger { get; set; }
    protected IBundleManager BundleManager { get; }
    protected IWebHostEnvironment HostingEnvironment { get; }
    protected SmartSoftwareBundlingOptions Options { get; }

    protected SmartSoftwareTagHelperResourceService(
        IBundleManager bundleManager,
        IOptions<SmartSoftwareBundlingOptions> options,
        IWebHostEnvironment hostingEnvironment)
    {
        BundleManager = bundleManager;
        HostingEnvironment = hostingEnvironment;
        Options = options.Value;

        Logger = NullLogger<SmartSoftwareTagHelperResourceService>.Instance;
    }

    public virtual async Task ProcessAsync(
        [NotNull] ViewContext viewContext,
        [NotNull] TagHelper tagHelper,
        [NotNull] TagHelperContext context,
        [NotNull] TagHelperOutput output,
        [NotNull] List<BundleTagHelperItem> bundleItems,
        string? bundleName = null)
    {
        Check.NotNull(viewContext, nameof(viewContext));
        Check.NotNull(context, nameof(context));
        Check.NotNull(output, nameof(output));
        Check.NotNull(bundleItems, nameof(bundleItems));

        var stopwatch = Stopwatch.StartNew();

        output.TagName = null;

        if (bundleName.IsNullOrEmpty())
        {
            bundleName = GenerateBundleName(bundleItems);
        }

        CreateBundle(bundleName!, bundleItems);

        var bundleFiles = await GetBundleFilesAsync(bundleName!);

        output.Content.Clear();

        foreach (var bundleFile in bundleFiles)
        {
            if (bundleFile.IsExternalFile)
            {
                AddHtmlTag(viewContext, tagHelper, context, output, bundleFile, null);
            }
            else
            {
                var file = HostingEnvironment.WebRootFileProvider.GetFileInfo(bundleFile.FileName);
                if (file == null || !file.Exists)
                {
                    Logger.LogError($"Could not find the bundle file '{bundleFile.FileName}' for the bundle '{bundleName}'!");
                    AddErrorScript(viewContext, tagHelper, context, output, bundleFile, bundleName!);
                    continue;
                }

                if (file.Length > 0)
                {
                    AddHtmlTag(viewContext, tagHelper, context, output, bundleFile, file);
                }
            }
        }

        stopwatch.Stop();
        Logger.LogDebug($"Added bundle '{bundleName}' to the page in {stopwatch.Elapsed.TotalMilliseconds:0.00} ms.");
    }

    protected abstract void CreateBundle(string bundleName, List<BundleTagHelperItem> bundleItems);

    protected abstract Task<IReadOnlyList<BundleFile>> GetBundleFilesAsync(string bundleName);

    protected abstract void AddHtmlTag(ViewContext viewContext, TagHelper tagHelper, TagHelperContext context, TagHelperOutput output, BundleFile file, IFileInfo? fileInfo = null);

    protected virtual void AddErrorScript(ViewContext viewContext, TagHelper tagHelper, TagHelperContext context, TagHelperOutput output, BundleFile file, string bundleName)
    {
        output.Content.AppendHtml($"<script>console.log(\"%cCould not find the bundle file '{file.FileName}' for the bundle '{bundleName}'!\", 'background: yellow; color: black; font-size:20px;');</script>{Environment.NewLine}");
    }

    protected virtual string GenerateBundleName(List<BundleTagHelperItem> bundleItems)
    {
        return bundleItems.JoinAsString("|").ToMd5();
    }
}