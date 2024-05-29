using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.ExceptionHandling;

public class NullUserExceptionInformer : IUserExceptionInformer, ISingletonDependency
{
    public void Inform(UserExceptionInformerContext context)
    {

    }

    public Task InformAsync(UserExceptionInformerContext context)
    {
        return Task.CompletedTask;
    }
}
