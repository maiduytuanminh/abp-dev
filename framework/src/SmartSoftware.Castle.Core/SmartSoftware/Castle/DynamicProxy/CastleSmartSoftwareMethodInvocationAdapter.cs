using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using SmartSoftware.DynamicProxy;

namespace SmartSoftware.Castle.DynamicProxy;

public class CastleSmartSoftwareMethodInvocationAdapter : CastleSmartSoftwareMethodInvocationAdapterBase, ISmartSoftwareMethodInvocation
{
    protected IInvocationProceedInfo ProceedInfo { get; }
    protected Func<IInvocation, IInvocationProceedInfo, Task> Proceed { get; }

    public CastleSmartSoftwareMethodInvocationAdapter(IInvocation invocation, IInvocationProceedInfo proceedInfo,
        Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        : base(invocation)
    {
        ProceedInfo = proceedInfo;
        Proceed = proceed;
    }

    public override async Task ProceedAsync()
    {
        await Proceed(Invocation, ProceedInfo);
    }
}
