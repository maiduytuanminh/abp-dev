using System.Threading.Tasks;

namespace SmartSoftware.Validation;

public interface IMethodInvocationValidator
{
    Task ValidateAsync(MethodInvocationValidationContext context);
}
