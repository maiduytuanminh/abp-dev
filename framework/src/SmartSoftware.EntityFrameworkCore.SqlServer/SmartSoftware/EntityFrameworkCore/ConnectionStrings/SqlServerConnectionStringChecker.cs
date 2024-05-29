using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SmartSoftware.Data;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.EntityFrameworkCore.ConnectionStrings;

[Dependency(ReplaceServices = true)]
public class SqlServerConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public virtual async Task<SmartSoftwareConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new SmartSoftwareConnectionStringCheckResult();
        var connString = new SqlConnectionStringBuilder(connectionString)
        {
            ConnectTimeout = 1
        };

        var oldDatabaseName = connString.InitialCatalog;
        connString.InitialCatalog = "master";

        try
        {
            await using var conn = new SqlConnection(connString.ConnectionString);
            await conn.OpenAsync();
            result.Connected = true;
            await conn.ChangeDatabaseAsync(oldDatabaseName);
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
