using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;

namespace SmartSoftware.FeatureManagement;

[RemoteService(Name = FeatureManagementRemoteServiceConsts.RemoteServiceName)]
[Area(FeatureManagementRemoteServiceConsts.ModuleName)]
[Route("api/feature-management/features")]
public class FeaturesController : SmartSoftwareControllerBase, IFeatureAppService
{
    protected IFeatureAppService FeatureAppService { get; }

    public FeaturesController(IFeatureAppService featureAppService)
    {
        FeatureAppService = featureAppService;
    }

    [HttpGet]
    public virtual Task<GetFeatureListResultDto> GetAsync(string providerName, string providerKey)
    {
        return FeatureAppService.GetAsync(providerName, providerKey);
    }

    [HttpPut]
    public virtual Task UpdateAsync(string providerName, string providerKey, UpdateFeaturesDto input)
    {
        return FeatureAppService.UpdateAsync(providerName, providerKey, input);
    }

    [HttpDelete]
    public virtual Task DeleteAsync(string providerName, string providerKey)
    {
        return FeatureAppService.DeleteAsync(providerName, providerKey);
    }
}
