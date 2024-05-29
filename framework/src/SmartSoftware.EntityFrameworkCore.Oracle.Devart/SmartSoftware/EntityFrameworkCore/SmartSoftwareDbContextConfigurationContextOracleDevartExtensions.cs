using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using Devart.Data.Oracle.Entity;
using SmartSoftware.EntityFrameworkCore.DependencyInjection;

namespace SmartSoftware.EntityFrameworkCore;

public static class SmartSoftwareDbContextConfigurationContextOracleDevartExtensions
{
    public static DbContextOptionsBuilder UseOracle(
       [NotNull] this SmartSoftwareDbContextConfigurationContext context,
       Action<OracleDbContextOptionsBuilder>? oracleOptionsAction = null,
       bool useExistingConnectionIfAvailable = false)
    {
        if (useExistingConnectionIfAvailable && context.ExistingConnection != null)
        {
            return context.DbContextOptions.UseOracle(context.ExistingConnection, optionsBuilder =>
            {
                optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                oracleOptionsAction?.Invoke(optionsBuilder);
            });
        }
        else
        {
            return context.DbContextOptions.UseOracle(context.ConnectionString, optionsBuilder =>
            {
                optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                oracleOptionsAction?.Invoke(optionsBuilder);
            });
        }
    }
}
