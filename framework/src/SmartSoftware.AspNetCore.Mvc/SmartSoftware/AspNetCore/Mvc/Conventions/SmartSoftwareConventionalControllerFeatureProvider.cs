using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SmartSoftware.AspNetCore.Mvc.Conventions;

public class SmartSoftwareConventionalControllerFeatureProvider : ControllerFeatureProvider
{
    private readonly ISmartSoftwareApplication _application;

    public SmartSoftwareConventionalControllerFeatureProvider(ISmartSoftwareApplication application)
    {
        _application = application;
    }

    protected override bool IsController(TypeInfo typeInfo)
    {
        //TODO: Move this to a lazy loaded field for efficiency.
        if (_application.ServiceProvider == null)
        {
            return false;
        }

        var configuration = _application.ServiceProvider
            .GetRequiredService<IOptions<SmartSoftwareAspNetCoreMvcOptions>>().Value
            .ConventionalControllers
            .ConventionalControllerSettings
            .GetSettingOrNull(typeInfo.AsType());

        return configuration != null;
    }
}
