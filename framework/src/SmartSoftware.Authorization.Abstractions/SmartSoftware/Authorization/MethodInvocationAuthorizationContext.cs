using System.Reflection;

namespace SmartSoftware.Authorization;

public class MethodInvocationAuthorizationContext
{
    public MethodInfo Method { get; }

    public MethodInvocationAuthorizationContext(MethodInfo method)
    {
        Method = method;
    }
}
