namespace SmartSoftware.BlobStoring;

public class SmartSoftwareBlobStoringOptions
{
    public BlobContainerConfigurations Containers { get; }

    public SmartSoftwareBlobStoringOptions()
    {
        Containers = new BlobContainerConfigurations();
    }
}
