using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.DynamicProxy;

namespace SmartSoftware.Validation;

public class ValidationInterceptor : SmartSoftwareInterceptor, ITransientDependency
{
    private readonly IMethodInvocationValidator _methodInvocationValidator;

    public ValidationInterceptor(IMethodInvocationValidator methodInvocationValidator)
    {
        _methodInvocationValidator = methodInvocationValidator;
    }

    public override async Task InterceptAsync(ISmartSoftwareMethodInvocation invocation)
    {
        await ValidateAsync(invocation);
        await invocation.ProceedAsync();
    }

    protected virtual async Task ValidateAsync(ISmartSoftwareMethodInvocation invocation)
    {
        await _methodInvocationValidator.ValidateAsync(
            new MethodInvocationValidationContext(
                invocation.TargetObject,
                invocation.Method,
                invocation.Arguments
            )
        );
    }
}
