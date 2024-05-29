using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Data;

public class DefaultConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public Task<SmartSoftwareConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        return Task.FromResult(new SmartSoftwareConnectionStringCheckResult
        {
            Connected = false,
            DatabaseExists = false
        });
    }
}
