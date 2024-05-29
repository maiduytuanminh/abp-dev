using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Dapr;

public class DaprApiTokenProvider : IDaprApiTokenProvider, ISingletonDependency
{
    protected SmartSoftwareDaprOptions Options { get; }

    public DaprApiTokenProvider(IOptions<SmartSoftwareDaprOptions> options)
    {
        Options = options.Value;
    }

    public virtual string? GetDaprApiToken()
    {
        return Options.DaprApiToken;
    }

    public virtual string? GetAppApiToken()
    {
        return Options.AppApiToken;
    }
}
