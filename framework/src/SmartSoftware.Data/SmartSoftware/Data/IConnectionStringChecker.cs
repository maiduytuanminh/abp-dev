using System.Threading.Tasks;

namespace SmartSoftware.Data;

public interface IConnectionStringChecker
{
    Task<SmartSoftwareConnectionStringCheckResult> CheckAsync(string connectionString);
}
