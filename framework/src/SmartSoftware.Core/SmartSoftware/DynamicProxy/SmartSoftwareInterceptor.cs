using System.Threading.Tasks;

namespace SmartSoftware.DynamicProxy;

public abstract class SmartSoftwareInterceptor : ISmartSoftwareInterceptor
{
    public abstract Task InterceptAsync(ISmartSoftwareMethodInvocation invocation);
}
