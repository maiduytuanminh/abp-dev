using System.Threading;
using System.Threading.Tasks;
using OpenIddict.Abstractions;

namespace SmartSoftware.OpenIddict.Applications;

public interface ISmartSoftwareOpenIdApplicationStore : IOpenIddictApplicationStore<OpenIddictApplicationModel>
{
    ValueTask<string> GetClientUriAsync(OpenIddictApplicationModel application, CancellationToken cancellationToken = default);

    ValueTask<string> GetLogoUriAsync(OpenIddictApplicationModel application, CancellationToken cancellationToken = default);
}
