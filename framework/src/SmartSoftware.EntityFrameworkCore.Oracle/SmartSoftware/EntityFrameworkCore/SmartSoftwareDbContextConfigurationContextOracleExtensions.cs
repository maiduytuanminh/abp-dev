using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using Oracle.EntityFrameworkCore.Infrastructure;
using SmartSoftware.EntityFrameworkCore.DependencyInjection;

namespace SmartSoftware.EntityFrameworkCore;

public static class SmartSoftwareDbContextConfigurationContextOracleExtensions
{
    public static DbContextOptionsBuilder UseOracle(
       [NotNull] this SmartSoftwareDbContextConfigurationContext context,
       Action<OracleDbContextOptionsBuilder>? oracleOptionsAction = null)
    {
        if (context.ExistingConnection != null)
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
