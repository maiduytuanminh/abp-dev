using System;

namespace SmartSoftware.DependencyInjection;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class DisablePropertyInjectionAttribute : Attribute
{

}
