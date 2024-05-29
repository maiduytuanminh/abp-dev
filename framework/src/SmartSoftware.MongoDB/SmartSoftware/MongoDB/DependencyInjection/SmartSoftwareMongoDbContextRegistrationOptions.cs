using System;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.MongoDB.DependencyInjection;

public class SmartSoftwareMongoDbContextRegistrationOptions : SmartSoftwareCommonDbContextRegistrationOptions, ISmartSoftwareMongoDbContextRegistrationOptionsBuilder
{
    public SmartSoftwareMongoDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
        : base(originalDbContextType, services)
    {
    }
}
