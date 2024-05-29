using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using SmartSoftware.AspNetCore.Filters;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationModels;

public class SmartSoftwareMvcActionDescriptorProvider : IActionDescriptorProvider
{
    public virtual int Order => -1000 + 10;

    public virtual void OnProvidersExecuting(ActionDescriptorProviderContext context)
    {
    }

    public virtual void OnProvidersExecuted(ActionDescriptorProviderContext context)
    {
        foreach (var action in context.Results.Where(x => x is ControllerActionDescriptor).Cast<ControllerActionDescriptor>())
        {
            var disableSmartSoftwareFeaturesAttribute = action.ControllerTypeInfo.GetCustomAttribute<DisableSmartSoftwareFeaturesAttribute>(true);
            if (disableSmartSoftwareFeaturesAttribute != null && disableSmartSoftwareFeaturesAttribute.DisableMvcFilters)
            {
                action.FilterDescriptors.RemoveAll(x => x.Filter is ServiceFilterAttribute serviceFilterAttribute &&
                                                        typeof(ISmartSoftwareFilter).IsAssignableFrom(serviceFilterAttribute.ServiceType));
            }
        }
    }
}
