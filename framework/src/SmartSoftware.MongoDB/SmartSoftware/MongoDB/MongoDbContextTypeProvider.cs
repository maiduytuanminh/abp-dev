using System;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.MongoDB;

public class MongoDbContextTypeProvider : IMongoDbContextTypeProvider, ITransientDependency
{
    private readonly SmartSoftwareMongoDbContextOptions _options;
    private readonly ICurrentTenant _currentTenant;

    public MongoDbContextTypeProvider(IOptions<SmartSoftwareMongoDbContextOptions> options, ICurrentTenant currentTenant)
    {
        _currentTenant = currentTenant;
        _options = options.Value;
    }

    public virtual Type GetDbContextType(Type dbContextType)
    {
        return _options.GetReplacedTypeOrSelf(dbContextType, _currentTenant.GetMultiTenancySide());
    }
}
