﻿using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.SignalR;

public class SmartSoftwareSignalRConventionalRegistrar : DefaultConventionalRegistrar
{
    protected override bool IsConventionalRegistrationDisabled(Type type)
    {
        return !IsHub(type) || base.IsConventionalRegistrationDisabled(type);
    }

    private static bool IsHub(Type type)
    {
        return typeof(Hub).IsAssignableFrom(type);
    }

    protected override ServiceLifetime? GetDefaultLifeTimeOrNull(Type type)
    {
        return ServiceLifetime.Transient;
    }
}
