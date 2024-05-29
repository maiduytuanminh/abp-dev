using JetBrains.Annotations;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SmartSoftware.EntityFrameworkCore;

public static class SmartSoftwareDbContextOptionsMySQLExtensions
{
    public static void UseMySQL(
            [NotNull] this SmartSoftwareDbContextOptions options,
            Action<MySqlDbContextOptionsBuilder>? mySQLOptionsAction = null)
    {
        options.Configure(context =>
        {
            context.UseMySQL(mySQLOptionsAction);
        });
    }

    public static void UseMySQL<TDbContext>(
        [NotNull] this SmartSoftwareDbContextOptions options,
        Action<MySqlDbContextOptionsBuilder>? mySQLOptionsAction = null)
        where TDbContext : SmartSoftwareDbContext<TDbContext>
    {
        options.Configure<TDbContext>(context =>
        {
            context.UseMySQL(mySQLOptionsAction);
        });
    }
}
