using System;
using System.Linq;
using SmartSoftware.Application.Services;
using SmartSoftware.Http.Client.ClientProxying;

namespace SmartSoftware.Http.Client.Web.Conventions;

public static class SmartSoftwareHttpClientProxyHelper
{
    public static bool IsClientProxyService(Type type)
    {
        return typeof(IApplicationService).IsAssignableFrom(type) &&
            type.GetBaseClasses().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ClientProxyBase<>));
    }
}
