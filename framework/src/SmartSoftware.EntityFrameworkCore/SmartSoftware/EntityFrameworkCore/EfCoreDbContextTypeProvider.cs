using System;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.EntityFrameworkCore;

public class EfCoreDbContextTypeProvider : IEfCoreDbContextTypeProvider, ITransientDependency
{
    private readonly SmartSoftwareDbContextOptions _options;
    private readonly ICurrentTenant _currentTenant;

    public EfCoreDbContextTypeProvider(IOptions<SmartSoftwareDbContextOptions> options, ICurrentTenant currentTenant)
    {
        _currentTenant = currentTenant;
        _options = options.Value;
    }

    public virtual Type GetDbContextType(Type dbContextType)
    {
        return _options.GetReplacedTypeOrSelf(dbContextType, _currentTenant.GetMultiTenancySide());
    }
}
