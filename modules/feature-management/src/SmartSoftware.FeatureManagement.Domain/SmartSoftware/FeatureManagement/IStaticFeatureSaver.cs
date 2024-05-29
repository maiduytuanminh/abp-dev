using System.Threading.Tasks;

namespace SmartSoftware.FeatureManagement;

public interface IStaticFeatureSaver
{
    Task SaveAsync();
}
