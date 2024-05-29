using System.Net.Http;
using System.Threading.Tasks;
using SmartSoftware.Http.Client.ClientProxying;

namespace SmartSoftware.Http.Client.DynamicProxying;

public class DynamicHttpProxyInterceptorClientProxy<TService> : ClientProxyBase<TService>
{
    public virtual async Task<T> CallRequestAsync<T>(ClientProxyRequestContext requestContext)
    {
        return await base.RequestAsync<T>(requestContext);
    }

    public virtual async Task<HttpContent> CallRequestAsync(ClientProxyRequestContext requestContext)
    {
        return await base.RequestAsync(requestContext);
    }
}
