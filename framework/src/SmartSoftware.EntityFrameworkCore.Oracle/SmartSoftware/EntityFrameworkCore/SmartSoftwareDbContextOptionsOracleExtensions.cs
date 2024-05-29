using JetBrains.Annotations;
using System;
using Oracle.EntityFrameworkCore.Infrastructure;

namespace SmartSoftware.EntityFrameworkCore;

public static class SmartSoftwareDbContextOptionsOracleExtensions
{
    public static void UseOracle(
            [NotNull] this SmartSoftwareDbContextOptions options,
            Action<OracleDbContextOptionsBuilder>? oracleOptionsAction = null)
    {
        options.Configure(context =>
        {
            context.UseOracle(oracleOptionsAction);
        });
    }

    public static void UseOracle<TDbContext>(
        [NotNull] this SmartSoftwareDbContextOptions options,
        Action<OracleDbContextOptionsBuilder>? oracleOptionsAction = null)
        where TDbContext : SmartSoftwareDbContext<TDbContext>
    {
        options.Configure<TDbContext>(context =>
        {
            context.UseOracle(oracleOptionsAction);
        });
    }
}
