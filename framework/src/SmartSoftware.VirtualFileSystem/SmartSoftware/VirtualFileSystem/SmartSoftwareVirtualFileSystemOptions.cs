namespace SmartSoftware.VirtualFileSystem;

public class SmartSoftwareVirtualFileSystemOptions
{
    public VirtualFileSetList FileSets { get; }

    public SmartSoftwareVirtualFileSystemOptions()
    {
        FileSets = new VirtualFileSetList();
    }
}
