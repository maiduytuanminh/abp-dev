using System.Threading.Tasks;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.Uow;
using SmartSoftware.Users;

namespace SmartSoftware.PermissionManagement.Identity;

public class UserDeletedEventHandler :
    IDistributedEventHandler<EntityDeletedEto<UserEto>>,
    ITransientDependency
{
    protected IPermissionManager PermissionManager { get; }

    public UserDeletedEventHandler(IPermissionManager permissionManager)
    {
        PermissionManager = permissionManager;
    }

    [UnitOfWork]
    public virtual async Task HandleEventAsync(EntityDeletedEto<UserEto> eventData)
    {
        await PermissionManager.DeleteAsync(UserPermissionValueProvider.ProviderName, eventData.Entity.Id.ToString());
    }
}
