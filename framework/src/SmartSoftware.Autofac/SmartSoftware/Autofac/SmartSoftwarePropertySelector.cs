using System.Collections.Generic;
using System.Reflection;
using Autofac.Core;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Autofac;

public class SmartSoftwarePropertySelector : DefaultPropertySelector
{
    public SmartSoftwarePropertySelector(bool preserveSetValues)
        : base(preserveSetValues)
    {
    }

    public override bool InjectProperty(PropertyInfo propertyInfo, object instance)
    {
        return propertyInfo.GetCustomAttributes(typeof(DisablePropertyInjectionAttribute), true).IsNullOrEmpty() &&
               base.InjectProperty(propertyInfo, instance);
    }

}
