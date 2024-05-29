using System.Collections.Generic;
using SmartSoftware.Collections;

namespace SmartSoftware.Authorization.Permissions;

public class SmartSoftwarePermissionOptions
{
    public ITypeList<IPermissionDefinitionProvider> DefinitionProviders { get; }

    public ITypeList<IPermissionValueProvider> ValueProviders { get; }

    public HashSet<string> DeletedPermissions { get; }

    public HashSet<string> DeletedPermissionGroups { get; }

    public SmartSoftwarePermissionOptions()
    {
        DefinitionProviders = new TypeList<IPermissionDefinitionProvider>();
        ValueProviders = new TypeList<IPermissionValueProvider>();

        DeletedPermissions = new HashSet<string>();
        DeletedPermissionGroups = new HashSet<string>();
    }
}
