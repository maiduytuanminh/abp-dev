﻿using System.Collections.Generic;

namespace SmartSoftware.PermissionManagement;

public class MultiplePermissionValueProviderGrantInfo
{
    public Dictionary<string, PermissionValueProviderGrantInfo> Result { get; }

    public MultiplePermissionValueProviderGrantInfo()
    {
        Result = new Dictionary<string, PermissionValueProviderGrantInfo>();
    }

    public MultiplePermissionValueProviderGrantInfo(string[] names)
    {
        Check.NotNull(names, nameof(names));

        Result = new Dictionary<string, PermissionValueProviderGrantInfo>();

        foreach (var name in names)
        {
            Result.Add(name, PermissionValueProviderGrantInfo.NonGranted);
        }
    }
}
