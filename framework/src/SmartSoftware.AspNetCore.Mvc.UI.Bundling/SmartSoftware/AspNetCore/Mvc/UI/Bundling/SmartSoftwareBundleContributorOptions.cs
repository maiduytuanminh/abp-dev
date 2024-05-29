using System;
using System.Collections.Concurrent;
using JetBrains.Annotations;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling;

public class SmartSoftwareBundleContributorOptions
{
    public ConcurrentDictionary<Type, BundleContributorCollection> AllExtensions { get; }

    public SmartSoftwareBundleContributorOptions()
    {
        AllExtensions = new ConcurrentDictionary<Type, BundleContributorCollection>();
    }

    public BundleContributorCollection Extensions<TContributor>()
    {
        return Extensions(typeof(TContributor));
    }

    public BundleContributorCollection Extensions([NotNull] Type contributorType)
    {
        Check.NotNull(contributorType, nameof(contributorType));

        return AllExtensions.GetOrAdd(
            contributorType,
            _ => new BundleContributorCollection()
        );
    }
}
