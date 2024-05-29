using System;
using System.Collections.Generic;
using SmartSoftware.Aspects;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Http.Modeling;

public class SmartSoftwareApiDescriptionModelOptions
{
    public HashSet<Type> IgnoredInterfaces { get; }

    public SmartSoftwareApiDescriptionModelOptions()
    {
        IgnoredInterfaces = new HashSet<Type>
            {
                typeof(ITransientDependency),
                typeof(ISingletonDependency),
                typeof(IDisposable),
                typeof(IAvoidDuplicateCrossCuttingConcerns)
            };
    }
}
