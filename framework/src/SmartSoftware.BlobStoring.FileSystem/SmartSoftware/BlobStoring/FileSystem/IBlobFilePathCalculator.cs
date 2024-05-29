namespace SmartSoftware.BlobStoring.FileSystem;

public interface IBlobFilePathCalculator
{
    string Calculate(BlobProviderArgs args);
}
