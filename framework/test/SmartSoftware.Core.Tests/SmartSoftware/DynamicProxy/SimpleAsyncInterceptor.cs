using System.Threading.Tasks;
using SmartSoftware.TestBase.Logging;

namespace SmartSoftware.DynamicProxy;

public class SimpleAsyncInterceptor : SmartSoftwareInterceptor
{
    public override async Task InterceptAsync(ISmartSoftwareMethodInvocation invocation)
    {
        await Task.Delay(5);
        (invocation.TargetObject as ICanLogOnObject)?.Logs?.Add($"{GetType().Name}_InterceptAsync_BeforeInvocation");
        await invocation.ProceedAsync();
        (invocation.TargetObject as ICanLogOnObject)?.Logs?.Add($"{GetType().Name}_InterceptAsync_AfterInvocation");
        await Task.Delay(5);
    }
}
