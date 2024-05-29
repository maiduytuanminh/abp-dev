using System;
using System.Collections.Generic;

namespace SmartSoftware.Security.Claims;

[Serializable]
public class SmartSoftwareDynamicClaimCacheItem
{
    public List<SmartSoftwareDynamicClaim> Claims { get; set; }

    public SmartSoftwareDynamicClaimCacheItem()
    {
        Claims = new List<SmartSoftwareDynamicClaim>();
    }

    public SmartSoftwareDynamicClaimCacheItem(List<SmartSoftwareDynamicClaim> claims)
    {
        Claims = claims;
    }

    public static string CalculateCacheKey(Guid userId, Guid? tenantId)
    {
        return $"{tenantId}-{userId}";
    }
}
