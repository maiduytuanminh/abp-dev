using System.Threading.Tasks;

namespace SmartSoftware.Data;

public interface IDataSeeder
{
    Task SeedAsync(DataSeedContext context);
}
