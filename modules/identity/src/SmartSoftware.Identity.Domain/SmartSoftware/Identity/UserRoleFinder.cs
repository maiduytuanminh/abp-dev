using System;
using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Identity;

public class UserRoleFinder : IUserRoleFinder, ITransientDependency
{
    protected IIdentityUserRepository IdentityUserRepository { get; }

    public UserRoleFinder(IIdentityUserRepository identityUserRepository)
    {
        IdentityUserRepository = identityUserRepository;
    }

    [Obsolete("Use GetRoleNamesAsync instead.")]
    public virtual async Task<string[]> GetRolesAsync(Guid userId)
    {
        return (await IdentityUserRepository.GetRoleNamesAsync(userId)).ToArray();
    }

    public async Task<string[]> GetRoleNamesAsync(Guid userId)
    {
        return (await IdentityUserRepository.GetRoleNamesAsync(userId)).ToArray();
    }
}
