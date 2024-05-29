using System.Data;

namespace SmartSoftware.OpenIddict;

public class SmartSoftwareOpenIddictStoreOptions
{
    public IsolationLevel? PruneIsolationLevel { get; set; }

    public IsolationLevel? DeleteIsolationLevel { get; set; }

    public SmartSoftwareOpenIddictStoreOptions()
    {
        PruneIsolationLevel = IsolationLevel.RepeatableRead;
        DeleteIsolationLevel = IsolationLevel.Serializable;
    }
}
