using JetBrains.Annotations;
using System;
using Devart.Data.Oracle.Entity;

namespace SmartSoftware.EntityFrameworkCore;

public static class SmartSoftwareDbContextOptionsOracleDevartExtensions
{
    public static void UseOracle(
            [NotNull] this SmartSoftwareDbContextOptions options,
            Action<OracleDbContextOptionsBuilder>? oracleOptionsAction = null,
            bool useExistingConnectionIfAvailable = false)
    {
        options.Configure(context =>
        {
            context.UseOracle(oracleOptionsAction, useExistingConnectionIfAvailable);
        });
    }

    public static void UseOracle<TDbContext>(
        [NotNull] this SmartSoftwareDbContextOptions options,
        Action<OracleDbContextOptionsBuilder>? oracleOptionsAction = null,
        bool useExistingConnectionIfAvailable = false)
        where TDbContext : SmartSoftwareDbContext<TDbContext>
    {
        options.Configure<TDbContext>(context =>
        {
            context.UseOracle(oracleOptionsAction, useExistingConnectionIfAvailable);
        });
    }
}
