using System;
using System.Collections.Generic;
using SmartSoftware.Domain.Entities;
using JetBrains.Annotations;

namespace SmartSoftware.Data;

public static class ConcurrencyStampExtensions
{
    public static void SetConcurrencyStampIfNotNull(this IHasConcurrencyStamp entity, string? concurrencyStamp)
    {
        if (!concurrencyStamp.IsNullOrEmpty())
        {
            entity.ConcurrencyStamp = concurrencyStamp!;
        }
    }
}
