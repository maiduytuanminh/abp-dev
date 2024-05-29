using System;

namespace SmartSoftware.AspNetCore.Security;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IgnoreSmartSoftwareSecurityHeaderAttribute : Attribute
{
    
}
