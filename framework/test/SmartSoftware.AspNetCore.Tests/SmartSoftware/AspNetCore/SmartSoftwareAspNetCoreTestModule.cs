using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Autofac;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.AspNetCore;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreTestBaseModule),
    typeof(SmartSoftwareAspNetCoreModule),
    typeof(SmartSoftwareAutofacModule)
    )]
public class SmartSoftwareAspNetCoreTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAspNetCoreTestModule>();
                //options.FileSets.ReplaceEmbeddedByPhysical<SmartSoftwareAspNetCoreTestModule>(FindProjectPath(hostingEnvironment));
            });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseCorrelationId();
        app.UseStaticFiles();
    }

    private string FindProjectPath(IWebHostEnvironment hostEnvironment)
    {
        var directory = new DirectoryInfo(hostEnvironment.ContentRootPath);

        while (directory != null && directory.Name != "SmartSoftware.AspNetCore.Tests")
        {
            directory = directory.Parent;
        }

        return directory?.FullName
               ?? throw new Exception("Could not find the project path by beginning from " + hostEnvironment.ContentRootPath + ", going through to parents and looking for SmartSoftware.AspNetCore.Tests");
    }
}
