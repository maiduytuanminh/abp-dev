using System;
using System.Collections.Generic;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.BackgroundWorkers.Quartz;

public class SmartSoftwareQuartzConventionalRegistrar : DefaultConventionalRegistrar
{
    protected override bool IsConventionalRegistrationDisabled(Type type)
    {
        return !typeof(IQuartzBackgroundWorker).IsAssignableFrom(type) || base.IsConventionalRegistrationDisabled(type);
    }

    protected override List<Type> GetExposedServiceTypes(Type type)
    {
        return new List<Type>()
            {
                typeof(IQuartzBackgroundWorker)
            };
    }
}
