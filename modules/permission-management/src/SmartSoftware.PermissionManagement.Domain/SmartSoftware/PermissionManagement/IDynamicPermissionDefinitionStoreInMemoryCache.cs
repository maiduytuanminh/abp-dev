using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SmartSoftware.Authorization.Permissions;

namespace SmartSoftware.PermissionManagement;

public interface IDynamicPermissionDefinitionStoreInMemoryCache
{
    string CacheStamp { get; set; }
    
    SemaphoreSlim SyncSemaphore { get; }
    
    DateTime? LastCheckTime { get; set; }

    Task FillAsync(
        List<PermissionGroupDefinitionRecord> permissionGroupRecords,
        List<PermissionDefinitionRecord> permissionRecords);

    PermissionDefinition GetPermissionOrNull(string name);
    
    IReadOnlyList<PermissionDefinition> GetPermissions();
    
    IReadOnlyList<PermissionGroupDefinition> GetGroups();
}