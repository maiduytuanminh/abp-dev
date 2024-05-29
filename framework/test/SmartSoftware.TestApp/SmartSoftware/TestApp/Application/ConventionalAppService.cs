using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartSoftware.Application.Services;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Uow;

namespace SmartSoftware.TestApp.Application;

public class ConventionalAppService : IApplicationService, ITransientDependency
{
    [Authorize]
    [UnitOfWork]
    public virtual Task GetAsync()
    {
        return Task.CompletedTask;
    }
}
