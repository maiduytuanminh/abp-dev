using System;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.MemoryDb.DependencyInjection;

public class SmartSoftwareMemoryDbContextRegistrationOptions : SmartSoftwareCommonDbContextRegistrationOptions, ISmartSoftwareMemoryDbContextRegistrationOptionsBuilder
{
    public SmartSoftwareMemoryDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
        : base(originalDbContextType, services)
    {
    }
}
