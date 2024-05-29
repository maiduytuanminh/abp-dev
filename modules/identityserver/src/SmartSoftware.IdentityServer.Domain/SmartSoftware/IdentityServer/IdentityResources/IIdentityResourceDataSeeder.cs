using System.Threading.Tasks;

namespace SmartSoftware.IdentityServer.IdentityResources;

public interface IIdentityResourceDataSeeder
{
    Task CreateStandardResourcesAsync();
}
