using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.MongoDB.ConnectionStrings;

[Dependency(ReplaceServices = true)]
public class MongoDBConnectionStringChecker : IConnectionStringChecker, ITransientDependency
{
    public virtual Task<SmartSoftwareConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        try
        {
            var mongoUrl = MongoUrl.Create(connectionString);
            var client = new MongoClient(mongoUrl);
            client.GetDatabase(mongoUrl.DatabaseName);
            return Task.FromResult(new SmartSoftwareConnectionStringCheckResult()
            {
                Connected = true,
                DatabaseExists = true
            });
        }
        catch (Exception e)
        {
            return Task.FromResult(new SmartSoftwareConnectionStringCheckResult());
        }
    }
}
