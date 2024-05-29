using System;
using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.Conventions;

namespace SmartSoftware.AspNetCore.Mvc;

public class SmartSoftwareAspNetCoreMvcOptions
{
    public bool? MinifyGeneratedScript { get; set; }

    public SmartSoftwareConventionalControllerOptions ConventionalControllers { get; }

    public HashSet<Type> IgnoredControllersOnModelExclusion { get; }

    public HashSet<Type> ControllersToRemove { get; }

    public bool ExposeIntegrationServices { get; set; } = false;

    public bool AutoModelValidation { get; set; }

    public bool EnableRazorRuntimeCompilationOnDevelopment { get; set; }

    public bool ChangeControllerModelApiExplorerGroupName { get; set; }

    public SmartSoftwareAspNetCoreMvcOptions()
    {
        ConventionalControllers = new SmartSoftwareConventionalControllerOptions();
        IgnoredControllersOnModelExclusion = new HashSet<Type>();
        ControllersToRemove = new HashSet<Type>();
        AutoModelValidation = true;
        EnableRazorRuntimeCompilationOnDevelopment = true;
        ChangeControllerModelApiExplorerGroupName = true;
    }
}
