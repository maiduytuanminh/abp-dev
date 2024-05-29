using System.Threading;
using System.Threading.Tasks;
using OpenIddict.Abstractions;

namespace SmartSoftware.OpenIddict.Applications;

public interface ISmartSoftwareApplicationManager : IOpenIddictApplicationManager
{
    ValueTask<string> GetClientUriAsync(object application, CancellationToken cancellationToken = default);

    ValueTask<string> GetLogoUriAsync(object application, CancellationToken cancellationToken = default);
}
