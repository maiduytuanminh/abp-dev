﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSoftware.MultiTenancy;

public interface ITenantStore
{
    Task<TenantConfiguration?> FindAsync(string normalizedName);

    Task<TenantConfiguration?> FindAsync(Guid id);

    Task<IReadOnlyList<TenantConfiguration>> GetListAsync(bool includeDetails = false);

    [Obsolete("Use FindAsync method.")]
    TenantConfiguration? Find(string normalizedName);

    [Obsolete("Use FindAsync method.")]
    TenantConfiguration? Find(Guid id);
}
