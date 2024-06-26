﻿using System;
using SmartSoftware.ObjectExtending.Modularity;

namespace SmartSoftware.ObjectExtending;

public class IdentityServerModuleExtensionConfiguration : ModuleExtensionConfiguration
{
    public IdentityServerModuleExtensionConfiguration ConfigureClient(
        Action<EntityExtensionConfiguration> configureAction)
    {
        return this.ConfigureEntity(
            IdentityServerModuleExtensionConsts.EntityNames.Client,
            configureAction
        );
    }

    public IdentityServerModuleExtensionConfiguration ConfigureApiResource(
        Action<EntityExtensionConfiguration> configureAction)
    {
        return this.ConfigureEntity(
            IdentityServerModuleExtensionConsts.EntityNames.ApiResource,
            configureAction
        );
    }

    public IdentityServerModuleExtensionConfiguration ConfigureApiScope(
        Action<EntityExtensionConfiguration> configureAction)
    {
        return this.ConfigureEntity(
            IdentityServerModuleExtensionConsts.EntityNames.ApiScope,
            configureAction
        );
    }

    public IdentityServerModuleExtensionConfiguration ConfigureIdentityResource(
        Action<EntityExtensionConfiguration> configureAction)
    {
        return this.ConfigureEntity(
            IdentityServerModuleExtensionConsts.EntityNames.IdentityResource,
            configureAction
        );
    }
}
