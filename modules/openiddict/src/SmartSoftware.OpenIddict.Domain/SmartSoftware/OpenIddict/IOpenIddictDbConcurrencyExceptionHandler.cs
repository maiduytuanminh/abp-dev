using System.Threading.Tasks;
using SmartSoftware.Data;

namespace SmartSoftware.OpenIddict;

public interface IOpenIddictDbConcurrencyExceptionHandler
{
    Task HandleAsync(SmartSoftwareDbConcurrencyException exception);
}
