using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using SmartSoftware.DynamicProxy;

namespace SmartSoftware.Castle.DynamicProxy;

public class CastleSmartSoftwareMethodInvocationAdapterWithReturnValue<TResult> : CastleSmartSoftwareMethodInvocationAdapterBase, ISmartSoftwareMethodInvocation
{
    protected IInvocationProceedInfo ProceedInfo { get; }
    protected Func<IInvocation, IInvocationProceedInfo, Task<TResult>> Proceed { get; }

    public CastleSmartSoftwareMethodInvocationAdapterWithReturnValue(IInvocation invocation,
        IInvocationProceedInfo proceedInfo,
        Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        : base(invocation)
    {
        ProceedInfo = proceedInfo;
        Proceed = proceed;
    }

    public override async Task ProceedAsync()
    {
        ReturnValue = (await Proceed(Invocation, ProceedInfo))!;
    }
}
