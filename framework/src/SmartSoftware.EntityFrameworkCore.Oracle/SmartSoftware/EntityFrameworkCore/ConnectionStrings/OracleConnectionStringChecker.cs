using System;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SmartSoftware.Data;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.EntityFrameworkCore.ConnectionStrings;

[Dependency(ReplaceServices = true)]
public class OracleConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public virtual async Task<SmartSoftwareConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new SmartSoftwareConnectionStringCheckResult();
        var connString = new OracleConnectionStringBuilder(connectionString)
        {
            ConnectionTimeout = 1
        };

        try
        {
            await using var conn = new OracleConnection(connString.ConnectionString);
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
