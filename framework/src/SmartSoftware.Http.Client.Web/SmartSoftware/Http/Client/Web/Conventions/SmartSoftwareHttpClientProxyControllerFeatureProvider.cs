using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace SmartSoftware.Http.Client.Web.Conventions;

public class SmartSoftwareHttpClientProxyControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        return SmartSoftwareHttpClientProxyHelper.IsClientProxyService(typeInfo);
    }
}
