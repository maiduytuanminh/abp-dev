using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SmartSoftware.Data;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.EntityFrameworkCore.ConnectionStrings;

[Dependency(ReplaceServices = true)]
public class SqliteConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public virtual async Task<SmartSoftwareConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new SmartSoftwareConnectionStringCheckResult();

        try
        {
            await using var conn = new SqliteConnection(connectionString);
            await conn.OpenAsync();
            result.Connected = true;
            result.DatabaseExists = true;

            await conn.CloseAsync();

            return result;
        }
        catch (Exception e)
        {
            return result;
        }
    }
}
