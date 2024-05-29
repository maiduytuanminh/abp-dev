using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling;

public class BundleConfigurationContext : IBundleConfigurationContext
{
    public List<BundleFile> Files { get; }

    public IFileProvider FileProvider { get; }

    public IServiceProvider ServiceProvider { get; }

    public ISmartSoftwareLazyServiceProvider LazyServiceProvider { get; }

    public BundleConfigurationContext(IServiceProvider serviceProvider, IFileProvider fileProvider)
    {
        Files = new List<BundleFile>();
        ServiceProvider = serviceProvider;
        LazyServiceProvider = ServiceProvider.GetRequiredService<ISmartSoftwareLazyServiceProvider>();
        FileProvider = fileProvider;
    }
}
