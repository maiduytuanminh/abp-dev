using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Filters;
using SmartSoftware.AspNetCore.Uow;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Uow;

namespace SmartSoftware.AspNetCore.Mvc.Uow;

public class SmartSoftwareUowPageFilter : IAsyncPageFilter, ISmartSoftwareFilter, ITransientDependency
{
    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        if (context.HandlerMethod == null || !context.ActionDescriptor.IsPageAction())
        {
            await next();
            return;
        }

        var methodInfo = context.HandlerMethod.MethodInfo;
        var unitOfWorkAttr = UnitOfWorkHelper.GetUnitOfWorkAttributeOrNull(methodInfo);

        context.HttpContext.Items["_SmartSoftwareActionInfo"] = new SmartSoftwareActionInfoInHttpContext
        {
            IsObjectResult = ActionResultHelper.IsObjectResult(context.HandlerMethod.MethodInfo.ReturnType, typeof(void))
        };

        if (unitOfWorkAttr?.IsDisabled == true)
        {
            await next();
            return;
        }

        var options = CreateOptions(context, unitOfWorkAttr);

        var unitOfWorkManager = context.GetRequiredService<IUnitOfWorkManager>();

        //Trying to begin a reserved UOW by SmartSoftwareUnitOfWorkMiddleware
        if (unitOfWorkManager.TryBeginReserved(UnitOfWork.UnitOfWorkReservationName, options))
        {
            var result = await next();
            if (Succeed(result))
            {
                await SaveChangesAsync(context, unitOfWorkManager);
            }
            else
            {
                await RollbackAsync(context, unitOfWorkManager);
            }

            return;
        }

        using (var uow = unitOfWorkManager.Begin(options))
        {
            var result = await next();
            if (Succeed(result))
            {
                await uow.CompleteAsync(context.HttpContext.RequestAborted);
            }
            else
            {
                await uow.RollbackAsync(context.HttpContext.RequestAborted);
            }
        }
    }

    private SmartSoftwareUnitOfWorkOptions CreateOptions(PageHandlerExecutingContext context, UnitOfWorkAttribute? unitOfWorkAttribute)
    {
        var options = new SmartSoftwareUnitOfWorkOptions();

        unitOfWorkAttribute?.SetOptions(options);

        if (unitOfWorkAttribute?.IsTransactional == null)
        {
            var ssUnitOfWorkDefaultOptions = context.GetRequiredService<IOptions<SmartSoftwareUnitOfWorkDefaultOptions>>().Value;
            options.IsTransactional = ssUnitOfWorkDefaultOptions.CalculateIsTransactional(
                autoValue: !string.Equals(context.HttpContext.Request.Method, HttpMethod.Get.Method, StringComparison.OrdinalIgnoreCase)
            );
        }

        return options;
    }

    private async Task RollbackAsync(PageHandlerExecutingContext context, IUnitOfWorkManager unitOfWorkManager)
    {
        var currentUow = unitOfWorkManager.Current;
        if (currentUow != null)
        {
            await currentUow.RollbackAsync(context.HttpContext.RequestAborted);
        }
    }

    private async Task SaveChangesAsync(PageHandlerExecutingContext context, IUnitOfWorkManager unitOfWorkManager)
    {
        var currentUow = unitOfWorkManager.Current;
        if (currentUow != null)
        {
            try
            {
                await currentUow.SaveChangesAsync(context.HttpContext.RequestAborted);
            }
            catch (Exception e)
            {
                await currentUow.RollbackAsync(context.HttpContext.RequestAborted);
                throw;
            }
        }
    }

    private static bool Succeed(PageHandlerExecutedContext result)
    {
        return result.Exception == null || result.ExceptionHandled;
    }
}
