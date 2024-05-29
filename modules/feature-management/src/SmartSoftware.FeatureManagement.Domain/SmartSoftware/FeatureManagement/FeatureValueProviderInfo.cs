﻿using System;
using JetBrains.Annotations;

namespace SmartSoftware.FeatureManagement;

[Serializable]
public class FeatureValueProviderInfo
{
    public string Name { get; }

    public string Key { get; }

    public FeatureValueProviderInfo([NotNull] string name, string key)
    {
        Check.NotNull(name, nameof(name));

        Name = name;
        Key = key;
    }
}
