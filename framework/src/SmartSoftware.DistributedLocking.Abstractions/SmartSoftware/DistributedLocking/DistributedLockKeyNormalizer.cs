using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.DistributedLocking;

public class DistributedLockKeyNormalizer : IDistributedLockKeyNormalizer, ITransientDependency
{
    protected SmartSoftwareDistributedLockOptions Options { get; }
    
    public DistributedLockKeyNormalizer(IOptions<SmartSoftwareDistributedLockOptions> options)
    {
        Options = options.Value;
    }
    
    public virtual string NormalizeKey(string name)
    {
        return $"{Options.KeyPrefix}{name}";
    }
}