using Aliyun.OSS;

namespace SmartSoftware.BlobStoring.Aliyun;

public interface IOssClientFactory
{
    IOss Create(AliyunBlobProviderConfiguration args);
}
