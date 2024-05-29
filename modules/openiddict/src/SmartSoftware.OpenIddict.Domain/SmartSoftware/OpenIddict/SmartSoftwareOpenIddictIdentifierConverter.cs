using System;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.OpenIddict;

public class SmartSoftwareOpenIddictIdentifierConverter : ITransientDependency
{
    public virtual Guid FromString(string identifier)
    {
        return string.IsNullOrEmpty(identifier) ? default : Guid.Parse(identifier);
    }

    public virtual string ToString(Guid identifier)
    {
        return identifier.ToString("D");
    }
}
