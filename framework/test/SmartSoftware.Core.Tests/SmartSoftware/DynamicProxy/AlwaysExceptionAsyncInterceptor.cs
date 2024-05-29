using System.Threading.Tasks;

namespace SmartSoftware.DynamicProxy;

public class AlwaysExceptionAsyncInterceptor : SmartSoftwareInterceptor
{
    public override Task InterceptAsync(ISmartSoftwareMethodInvocation invocation)
    {
        throw new SmartSoftwareException("This interceptor should not be executed!");
    }
}
