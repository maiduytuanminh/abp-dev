using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using SmartSoftware.DynamicProxy;

namespace SmartSoftware.Castle.DynamicProxy;

public class CastleAsyncSmartSoftwareInterceptorAdapter<TInterceptor> : AsyncInterceptorBase
    where TInterceptor : ISmartSoftwareInterceptor
{
    private readonly TInterceptor _ssInterceptor;

    public CastleAsyncSmartSoftwareInterceptorAdapter(TInterceptor ssInterceptor)
    {
        _ssInterceptor = ssInterceptor;
    }

    protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
    {
        await _ssInterceptor.InterceptAsync(
            new CastleSmartSoftwareMethodInvocationAdapter(invocation, proceedInfo, proceed)
        );
    }

    protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
    {
        var adapter = new CastleSmartSoftwareMethodInvocationAdapterWithReturnValue<TResult>(invocation, proceedInfo, proceed);

        await _ssInterceptor.InterceptAsync(
            adapter
        );

        return (TResult)adapter.ReturnValue;
    }
}
