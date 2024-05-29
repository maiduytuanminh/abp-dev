using System.Reflection;

namespace SmartSoftware.Features;

public class MethodInvocationFeatureCheckerContext
{
    public MethodInfo Method { get; }

    public MethodInvocationFeatureCheckerContext(MethodInfo method)
    {
        Method = method;
    }
}
