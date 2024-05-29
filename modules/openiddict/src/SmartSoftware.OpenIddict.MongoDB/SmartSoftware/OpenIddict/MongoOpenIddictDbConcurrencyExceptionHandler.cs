using System.Threading.Tasks;
using SmartSoftware.Data;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.OpenIddict;

public class MongoOpenIddictDbConcurrencyExceptionHandler: IOpenIddictDbConcurrencyExceptionHandler, ITransientDependency
{
    public virtual Task HandleAsync(SmartSoftwareDbConcurrencyException exception)
    {
        return Task.CompletedTask;
    }
}
