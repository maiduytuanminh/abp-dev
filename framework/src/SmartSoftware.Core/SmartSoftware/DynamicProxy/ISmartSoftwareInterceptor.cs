using System.Threading.Tasks;

namespace SmartSoftware.DynamicProxy;

public interface ISmartSoftwareInterceptor
{
    Task InterceptAsync(ISmartSoftwareMethodInvocation invocation);
}
