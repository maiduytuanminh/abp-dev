using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartSoftware.Authorization.Permissions;

namespace SmartSoftware.PermissionManagement;

public interface IPermissionDefinitionSerializer
{
    Task<(PermissionGroupDefinitionRecord[], PermissionDefinitionRecord[])>
        SerializeAsync(IEnumerable<PermissionGroupDefinition> permissionGroups);

    Task<PermissionGroupDefinitionRecord> SerializeAsync(
        PermissionGroupDefinition permissionGroup);

    Task<PermissionDefinitionRecord> SerializeAsync(
        PermissionDefinition permission,
        [CanBeNull] PermissionGroupDefinition permissionGroup);
}