using System.Threading.Tasks;
using JetBrains.Annotations;

namespace SmartSoftware.Features;

public interface IFeatureValueProvider
{
    string Name { get; }

    Task<string?> GetOrNullAsync([NotNull] FeatureDefinition feature);
}
