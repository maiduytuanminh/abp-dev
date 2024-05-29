﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SmartSoftware.EntityFrameworkCore;

/// <summary>
/// Avoid unit test caching the configure of the entity.
/// OnModelCreating will be executed multiple times
/// </summary>
public class UnitTestModelCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(DbContext context)
    {
        return context;
    }

    public virtual object Create(DbContext context, bool designTime)
    {
        return context;
    }
}
