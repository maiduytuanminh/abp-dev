using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.Settings;
using SmartSoftware.Uow;
using SmartSoftware.Users;

namespace SmartSoftware.SettingManagement;

public class UserDeletedEventHandler :
    IDistributedEventHandler<EntityDeletedEto<UserEto>>,
    ITransientDependency
{
    protected ISettingManager SettingManager { get; }

    public UserDeletedEventHandler(ISettingManager settingManager)
    {
        SettingManager = settingManager;
    }

    [UnitOfWork]
    public virtual async Task HandleEventAsync(EntityDeletedEto<UserEto> eventData)
    {
        await SettingManager.DeleteAsync(UserSettingValueProvider.ProviderName, eventData.Entity.Id.ToString());
    }
}
