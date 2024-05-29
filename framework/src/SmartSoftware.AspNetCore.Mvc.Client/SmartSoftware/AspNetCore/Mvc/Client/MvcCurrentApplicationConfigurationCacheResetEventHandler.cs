using System.Threading.Tasks;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.EventBus;
using SmartSoftware.Users;

namespace SmartSoftware.AspNetCore.Mvc.Client;

public class MvcCurrentApplicationConfigurationCacheResetEventHandler :
    ILocalEventHandler<CurrentApplicationConfigurationCacheResetEventData>,
    ITransientDependency
{
    protected ICurrentUser CurrentUser { get; }
    protected IDistributedCache<ApplicationConfigurationDto> Cache { get; }

    public MvcCurrentApplicationConfigurationCacheResetEventHandler(ICurrentUser currentUser,
        IDistributedCache<ApplicationConfigurationDto> cache)
    {
        CurrentUser = currentUser;
        Cache = cache;
    }

    public virtual async Task HandleEventAsync(CurrentApplicationConfigurationCacheResetEventData eventData)
    {
        await Cache.RemoveAsync(CreateCacheKey());
    }

    protected virtual string CreateCacheKey()
    {
        return MvcCachedApplicationConfigurationClientHelper.CreateCacheKey(CurrentUser);
    }
}
