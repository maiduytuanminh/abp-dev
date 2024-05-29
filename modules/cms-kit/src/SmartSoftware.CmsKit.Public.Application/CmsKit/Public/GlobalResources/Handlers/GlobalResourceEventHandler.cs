using System.Threading.Tasks;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities.Events;
using SmartSoftware.EventBus;
using SmartSoftware.ObjectMapping;
using SmartSoftware.CmsKit.GlobalResources;

namespace SmartSoftware.CmsKit.Public.GlobalResources.Handlers;

public class GlobalResourceEventHandler :
    ILocalEventHandler<EntityUpdatedEventData<GlobalResource>>,
    ITransientDependency
{
    public IObjectMapper ObjectMapper { get; }
    private readonly IDistributedCache<GlobalResourceDto> _resourceCache;

    public GlobalResourceEventHandler(
        IDistributedCache<GlobalResourceDto> resourceCache,
        IObjectMapper objectMapper)
    {
        ObjectMapper = objectMapper;
        _resourceCache = resourceCache;
    }

    public async Task HandleEventAsync(EntityUpdatedEventData<GlobalResource> eventData)
    {
        await _resourceCache.SetAsync(
            eventData.Entity.Name,
            ObjectMapper.Map<GlobalResource, GlobalResourceDto>(eventData.Entity));
    }
}