using System;
using System.IO;
using SmartSoftware.IO;
using SmartSoftware.Modularity;

namespace SmartSoftware.BlobStoring.FileSystem;

[DependsOn(
    typeof(SmartSoftwareBlobStoringFileSystemModule),
    typeof(SmartSoftwareBlobStoringTestModule)
    )]
public class SmartSoftwareBlobStoringFileSystemTestModule : SmartSoftwareModule
{
    private readonly string _testDirectoryPath;

    public SmartSoftwareBlobStoringFileSystemTestModule()
    {
        _testDirectoryPath = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid().ToString("N")
        );
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureAll((containerName, containerConfiguration) =>
            {
                containerConfiguration.UseFileSystem(fileSystem =>
                {
                    fileSystem.BasePath = _testDirectoryPath;
                });
            });
        });
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        DirectoryHelper.DeleteIfExists(_testDirectoryPath, true);
    }
}
