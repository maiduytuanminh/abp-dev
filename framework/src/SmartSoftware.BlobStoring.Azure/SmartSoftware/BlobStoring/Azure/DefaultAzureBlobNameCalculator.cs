﻿using SmartSoftware.DependencyInjection;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.BlobStoring.Azure;

public class DefaultAzureBlobNameCalculator : IAzureBlobNameCalculator, ITransientDependency
{
    protected ICurrentTenant CurrentTenant { get; }

    public DefaultAzureBlobNameCalculator(ICurrentTenant currentTenant)
    {
        CurrentTenant = currentTenant;
    }

    public virtual string Calculate(BlobProviderArgs args)
    {
        return CurrentTenant.Id == null
            ? $"host/{args.BlobName}"
            : $"tenants/{CurrentTenant.Id.Value.ToString("D")}/{args.BlobName}";
    }
}
