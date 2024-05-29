using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Data;
using SmartSoftware.Uow;
using SmartSoftware.Users;

namespace SmartSoftware.AspNetCore.Mvc.ExceptionHandling;

public class ExceptionHandingUnitOfWork : UnitOfWork
{
    public ExceptionHandingUnitOfWork(
        IServiceProvider serviceProvider,
        IUnitOfWorkEventPublisher unitOfWorkEventPublisher,
        IOptions<SmartSoftwareUnitOfWorkDefaultOptions> options)
        : base(serviceProvider, unitOfWorkEventPublisher, options)
    {

    }
    public async override Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (ServiceProvider.GetRequiredService<ICurrentUser>().Id == Guid.Empty)
        {
            throw new SmartSoftwareDbConcurrencyException();
        }

        await base.SaveChangesAsync(cancellationToken);
    }
}
