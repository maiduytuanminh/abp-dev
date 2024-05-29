namespace SmartSoftware.BlobStoring.Minio;

public interface IMinioBlobNameCalculator
{
    string Calculate(BlobProviderArgs args);
}
