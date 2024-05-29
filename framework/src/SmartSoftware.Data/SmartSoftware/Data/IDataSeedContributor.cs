using System.Threading.Tasks;

namespace SmartSoftware.Data;

public interface IDataSeedContributor
{
    Task SeedAsync(DataSeedContext context);
}
