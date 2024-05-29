using System.Threading.Tasks;
using JetBrains.Annotations;

namespace SmartSoftware.MultiTenancy;

public interface IMultiTenantUrlProvider
{
    Task<string> GetUrlAsync(string templateUrl);
}