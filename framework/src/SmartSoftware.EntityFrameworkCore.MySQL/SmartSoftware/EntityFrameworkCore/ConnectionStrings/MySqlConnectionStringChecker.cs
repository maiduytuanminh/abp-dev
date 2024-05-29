using System;
using System.Threading.Tasks;
using MySqlConnector;
using SmartSoftware.Data;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.EntityFrameworkCore.ConnectionStrings;

[Dependency(ReplaceServices = true)]
public class MySqlConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public virtual async Task<SmartSoftwareConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new SmartSoftwareConnectionStringCheckResult();
        var connString = new MySqlConnectionStringBuilder(connectionString)
        {
            ConnectionLifeTime = 1
        };

        var oldDatabaseName = connString.Database;
        connString.Database = "mysql";

        try
        {
            await using var conn = new MySqlConnection(connString.ConnectionString);
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
