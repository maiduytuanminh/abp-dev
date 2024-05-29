using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.DependencyInjection;

public class SmartSoftwareWebAssemblyConventionalRegistrar : DefaultConventionalRegistrar
{
    protected override bool IsConventionalRegistrationDisabled(Type type)
    {
        return !IsComponent(type) || base.IsConventionalRegistrationDisabled(type);
    }

    private static bool IsComponent(Type type)
    {
        return typeof(ComponentBase).IsAssignableFrom(type);
    }

    protected override ServiceLifetime? GetDefaultLifeTimeOrNull(Type type)
    {
        return ServiceLifetime.Transient;
    }
}
