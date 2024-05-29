using System;
using System.Collections.Generic;
using System.Text;
using SmartSoftware.Caching;

namespace SmartSoftware.BlobStoring.Aliyun;

[Serializable]
public class AliyunTemporaryCredentialsCacheItem
{
    public string AccessKeyId { get; set; } = default!;

    public string AccessKeySecret { get; set; } = default!;

    public string SecurityToken { get; set; } = default!;

    public AliyunTemporaryCredentialsCacheItem()
    {

    }

    public AliyunTemporaryCredentialsCacheItem(string accessKeyId, string accessKeySecret, string securityToken)
    {
        AccessKeyId = accessKeyId;
        AccessKeySecret = accessKeySecret;
        SecurityToken = securityToken;
    }
}
