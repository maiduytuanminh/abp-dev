using System.Threading.Tasks;
using SmartSoftware.Application.Services;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.GlobalResources;

namespace SmartSoftware.CmsKit.Public.GlobalResources;

[RequiresFeature(CmsKitFeatures.GlobalResourceEnable)]
[RequiresGlobalFeature(typeof(GlobalResourcesFeature))]
public class GlobalResourcePublicAppService : ApplicationService, IGlobalResourcePublicAppService
{
    public GlobalResourceManager GlobalResourceManager { get; }

    public GlobalResourcePublicAppService(GlobalResourceManager globalResourceManager)
    {
        GlobalResourceManager = globalResourceManager;
    }

    public virtual async Task<GlobalResourceDto> GetGlobalScriptAsync()
    {
        var globalScript = await GlobalResourceManager.GetGlobalScriptAsync();

        return ObjectMapper.Map<GlobalResource, GlobalResourceDto>(globalScript);
    }

    public virtual async Task<GlobalResourceDto> GetGlobalStyleAsync()
    {
        var globalStyle = await GlobalResourceManager.GetGlobalStyleAsync();

        return ObjectMapper.Map<GlobalResource, GlobalResourceDto>(globalStyle);
    }
}