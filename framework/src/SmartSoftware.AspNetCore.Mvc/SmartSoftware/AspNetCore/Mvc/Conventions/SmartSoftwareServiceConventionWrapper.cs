using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.Conventions;

[DisableConventionalRegistration]
public class SmartSoftwareServiceConventionWrapper : IApplicationModelConvention
{
    private readonly Lazy<ISmartSoftwareServiceConvention> _convention;

    public SmartSoftwareServiceConventionWrapper(IServiceCollection services)
    {
        _convention = services.GetRequiredServiceLazy<ISmartSoftwareServiceConvention>();
    }

    public void Apply(ApplicationModel application)
    {
        _convention.Value.Apply(application);
    }
}
