using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.BlobStoring;

public class DefaultBlobContainerConfigurationProvider : IBlobContainerConfigurationProvider, ITransientDependency
{
    protected SmartSoftwareBlobStoringOptions Options { get; }

    public DefaultBlobContainerConfigurationProvider(IOptions<SmartSoftwareBlobStoringOptions> options)
    {
        Options = options.Value;
    }

    public virtual BlobContainerConfiguration Get(string name)
    {
        return Options.Containers.GetConfiguration(name);
    }
}
