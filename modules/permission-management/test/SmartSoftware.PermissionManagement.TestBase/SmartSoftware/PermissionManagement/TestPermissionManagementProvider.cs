using System;
using System.Collections.Generic;
using System.Text;
using SmartSoftware.Guids;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.PermissionManagement;

public class TestPermissionManagementProvider : PermissionManagementProvider
{
    public override string Name => "Test";

    public TestPermissionManagementProvider(
        IPermissionGrantRepository permissionGrantRepository,
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant)
        : base(
            permissionGrantRepository,
            guidGenerator,
            currentTenant)
    {

    }
}
