using System;
using SmartSoftware.Collections;
using SmartSoftware.DynamicProxy;

namespace SmartSoftware.DependencyInjection;

public interface IOnServiceRegistredContext
{
    ITypeList<ISmartSoftwareInterceptor> Interceptors { get; }

    Type ImplementationType { get; }
}
