using System.Threading.Tasks;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities.Events;
using SmartSoftware.EventBus;
using SmartSoftware.IdentityServer.Clients;

namespace SmartSoftware.IdentityServer;

public class AllowedCorsOriginsCacheItemInvalidator :
    ILocalEventHandler<EntityChangedEventData<Client>>,
    ILocalEventHandler<EntityChangedEventData<ClientCorsOrigin>>,
    ITransientDependency
{
    protected IDistributedCache<AllowedCorsOriginsCacheItem> Cache { get; }

    public AllowedCorsOriginsCacheItemInvalidator(IDistributedCache<AllowedCorsOriginsCacheItem> cache)
    {
        Cache = cache;
    }

    public virtual async Task HandleEventAsync(EntityChangedEventData<Client> eventData)
    {
        await Cache.RemoveAsync(AllowedCorsOriginsCacheItem.AllOrigins);
    }

    public virtual async Task HandleEventAsync(EntityChangedEventData<ClientCorsOrigin> eventData)
    {
        await Cache.RemoveAsync(AllowedCorsOriginsCacheItem.AllOrigins);
    }
}
