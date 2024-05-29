using System.Threading.Tasks;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.EventBus;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.Identity;
using SmartSoftware.Uow;

namespace SmartSoftware.PermissionManagement.Identity;

public class RoleDeletedEventHandler :
    IDistributedEventHandler<EntityDeletedEto<IdentityRoleEto>>,
    ITransientDependency
{
    protected IPermissionManager PermissionManager { get; }

    public RoleDeletedEventHandler(IPermissionManager permissionManager)
    {
        PermissionManager = permissionManager;
    }

    [UnitOfWork]
    public virtual async Task HandleEventAsync(EntityDeletedEto<IdentityRoleEto> eventData)
    {
        await PermissionManager.DeleteAsync(RolePermissionValueProvider.ProviderName, eventData.Entity.Name);
    }
}
