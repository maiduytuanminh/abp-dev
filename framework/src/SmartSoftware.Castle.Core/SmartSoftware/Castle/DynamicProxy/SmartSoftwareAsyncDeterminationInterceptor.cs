using Castle.DynamicProxy;
using SmartSoftware.DynamicProxy;

namespace SmartSoftware.Castle.DynamicProxy;

public class SmartSoftwareAsyncDeterminationInterceptor<TInterceptor> : AsyncDeterminationInterceptor
    where TInterceptor : ISmartSoftwareInterceptor
{
    public SmartSoftwareAsyncDeterminationInterceptor(TInterceptor ssInterceptor)
        : base(new CastleAsyncSmartSoftwareInterceptorAdapter<TInterceptor>(ssInterceptor))
    {

    }
}
