using System.Threading.Tasks;

namespace SmartSoftware.Features;

public interface IMethodInvocationFeatureCheckerService
{
    Task CheckAsync(
        MethodInvocationFeatureCheckerContext context
    );
}
