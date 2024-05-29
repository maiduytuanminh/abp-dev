﻿using System;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.FeatureManagement;

[Serializable]
[IgnoreMultiTenancy]
public class FeatureValueCacheItem
{
    public string Value { get; set; }

    public FeatureValueCacheItem()
    {

    }

    public FeatureValueCacheItem(string value)
    {
        Value = value;
    }

    public static string CalculateCacheKey(string name, string providerName, string providerKey)
    {
        return "pn:" + providerName + ",pk:" + providerKey + ",n:" + name;
    }
}
