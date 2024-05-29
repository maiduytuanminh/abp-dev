using System;
using SmartSoftware.Modularity;

namespace SmartSoftware.BlobStoring.FileSystem;

[DependsOn(
    typeof(SmartSoftwareBlobStoringModule)
    )]
public class SmartSoftwareBlobStoringFileSystemModule : SmartSoftwareModule
{

}
