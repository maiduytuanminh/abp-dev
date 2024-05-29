using System.Threading.Tasks;

namespace SmartSoftware.Features;

public interface IMethodFeatureTestService
{
    Task<int> Feature1Async();

    Task NonFeatureAsync();
}
