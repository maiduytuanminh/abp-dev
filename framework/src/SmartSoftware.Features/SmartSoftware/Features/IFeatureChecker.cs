using JetBrains.Annotations;
using System.Threading.Tasks;

namespace SmartSoftware.Features;

public interface IFeatureChecker
{
    Task<string?> GetOrNullAsync([NotNull] string name);

    Task<bool> IsEnabledAsync(string name);
}
