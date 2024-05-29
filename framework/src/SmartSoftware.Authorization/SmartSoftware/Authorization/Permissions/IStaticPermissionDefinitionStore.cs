using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSoftware.Authorization.Permissions;

public interface IStaticPermissionDefinitionStore
{
    Task<PermissionDefinition?> GetOrNullAsync(string name);

    Task<IReadOnlyList<PermissionDefinition>> GetPermissionsAsync();
    
    Task<IReadOnlyList<PermissionGroupDefinition>> GetGroupsAsync();
}