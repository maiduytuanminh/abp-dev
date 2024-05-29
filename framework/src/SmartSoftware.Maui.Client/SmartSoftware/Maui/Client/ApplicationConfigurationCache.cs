﻿using System;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Maui.Client;
public class ApplicationConfigurationCache : ISingletonDependency
{
    protected ApplicationConfigurationDto? Configuration { get; set; }
    public event Action? ApplicationConfigurationChanged;
    public virtual ApplicationConfigurationDto? Get()
    {
        return Configuration;
    }

    public void Set(ApplicationConfigurationDto configuration)
    {
        Configuration = configuration;
        ApplicationConfigurationChanged?.Invoke();
    }
}