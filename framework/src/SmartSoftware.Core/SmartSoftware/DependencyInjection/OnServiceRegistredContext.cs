using System;
using JetBrains.Annotations;
using SmartSoftware.Collections;
using SmartSoftware.DynamicProxy;

namespace SmartSoftware.DependencyInjection;

public class OnServiceRegistredContext : IOnServiceRegistredContext
{
    public virtual ITypeList<ISmartSoftwareInterceptor> Interceptors { get; }

    public virtual Type ServiceType { get; }

    public virtual Type ImplementationType { get; }

    public OnServiceRegistredContext(Type serviceType, [NotNull] Type implementationType)
    {
        ServiceType = Check.NotNull(serviceType, nameof(serviceType));
        ImplementationType = Check.NotNull(implementationType, nameof(implementationType));

        Interceptors = new TypeList<ISmartSoftwareInterceptor>();
    }
}
