using System.Reflection;

namespace SmartSoftware.Validation;

public class MethodInvocationValidationContext : SmartSoftwareValidationResult
{
    public object TargetObject { get; }

    public MethodInfo Method { get; }

    public object[] ParameterValues { get; }

    public ParameterInfo[] Parameters { get; }

    public MethodInvocationValidationContext(object targetObject, MethodInfo method, object[] parameterValues)
    {
        TargetObject = targetObject;
        Method = method;
        ParameterValues = parameterValues;
        Parameters = method.GetParameters();
    }
}
