using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware;
using SmartSoftware.AspNetCore;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Modularity.PlugIns;

var builder = WebApplication.CreateBuilder();
await builder.RunSmartSoftwareModuleAsync<SmartSoftwareAspNetCoreMvcTestModule>(options =>
{
    var hostEnvironment = options.Services.GetHostingEnvironment();
    var currentDirectory = hostEnvironment.ContentRootPath;
    var plugDllInPath = "";

    for (var i = 0; i < 10; i++)
    {
        var parentDirectory = new DirectoryInfo(currentDirectory).Parent;
        if (parentDirectory == null)
        {
            break;
        }

        if (parentDirectory.Name == "test")
        {
#if DEBUG
            plugDllInPath = Path.Combine(parentDirectory.FullName, "SmartSoftware.AspNetCore.Mvc.PlugIn", "bin", "Debug", "net8.0");
#else
            plugDllInPath = Path.Combine(parentDirectory.FullName, "SmartSoftware.AspNetCore.Mvc.PlugIn", "bin", "Release", "net8.0");
#endif
            break;
        }

        currentDirectory = parentDirectory.FullName;
    }

    if (plugDllInPath.IsNullOrWhiteSpace())
    {
        throw new SmartSoftwareException("Could not find the plug DLL path!");
    }

    options.PlugInSources.AddFolder(plugDllInPath);
});

public partial class Program
{
}
