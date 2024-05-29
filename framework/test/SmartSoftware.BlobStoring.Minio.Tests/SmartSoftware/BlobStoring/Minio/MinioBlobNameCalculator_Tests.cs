﻿using System;
using Shouldly;
using SmartSoftware.MultiTenancy;
using Xunit;

namespace SmartSoftware.BlobStoring.Minio;

public class MinioBlobNameCalculator_Tests : SmartSoftwareBlobStoringMinioTestCommonBase
{
    private readonly IMinioBlobNameCalculator _calculator;
    private readonly ICurrentTenant _currentTenant;

    private const string MinioSeparator = "/";

    public MinioBlobNameCalculator_Tests()
    {
        _calculator = GetRequiredService<IMinioBlobNameCalculator>();
        _currentTenant = GetRequiredService<ICurrentTenant>();
    }

    [Fact]
    public void Default_Settings()
    {
        _calculator.Calculate(
            GetArgs("my-container", "my-blob")
        ).ShouldBe($"host{MinioSeparator}my-blob");
    }

    [Fact]
    public void Default_Settings_With_TenantId()
    {
        var tenantId = Guid.NewGuid();

        using (_currentTenant.Change(tenantId))
        {
            _calculator.Calculate(
                GetArgs("my-container", "my-blob")
            ).ShouldBe($"tenants{MinioSeparator}{tenantId:D}{MinioSeparator}my-blob");
        }
    }

    private static BlobProviderArgs GetArgs(
        string containerName,
        string blobName)
    {
        return new BlobProviderGetArgs(
            containerName,
            new BlobContainerConfiguration().UseMinio(x =>
            {
                x.BucketName = containerName;
            }),
            blobName
        );
    }
}
