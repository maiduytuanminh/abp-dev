using SmartSoftware.Testing;

namespace SmartSoftware.BlobStoring.FileSystem;

public abstract class SmartSoftwareBlobStoringFileSystemTestBase : SmartSoftwareIntegratedTest<SmartSoftwareBlobStoringFileSystemTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
