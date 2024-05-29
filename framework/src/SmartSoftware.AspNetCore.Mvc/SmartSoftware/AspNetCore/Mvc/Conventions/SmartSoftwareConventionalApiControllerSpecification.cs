using Asp.Versioning.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;

namespace SmartSoftware.AspNetCore.Mvc.Conventions;

public class SmartSoftwareConventionalApiControllerSpecification : IApiControllerSpecification
{
    private readonly SmartSoftwareAspNetCoreMvcOptions _options;

    public SmartSoftwareConventionalApiControllerSpecification(IOptions<SmartSoftwareAspNetCoreMvcOptions> options)
    {
        _options = options.Value;
    }

    public bool IsSatisfiedBy(ControllerModel controller)
    {
        var configuration = _options
            .ConventionalControllers
            .ConventionalControllerSettings
            .GetSettingOrNull(controller.ControllerType.AsType());

        return configuration != null;
    }
}
