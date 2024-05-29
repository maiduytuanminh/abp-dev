﻿using SmartSoftware.DependencyInjection;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.BlobStoring.Aws;

public class DefaultAwsBlobNameCalculator : IAwsBlobNameCalculator, ITransientDependency
{
    protected ICurrentTenant CurrentTenant { get; }

    public DefaultAwsBlobNameCalculator(ICurrentTenant currentTenant)
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
