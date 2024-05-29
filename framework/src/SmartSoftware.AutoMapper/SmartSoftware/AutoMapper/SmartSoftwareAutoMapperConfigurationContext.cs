using System;
using AutoMapper;

namespace SmartSoftware.AutoMapper;

public class SmartSoftwareAutoMapperConfigurationContext : ISmartSoftwareAutoMapperConfigurationContext
{
    public IMapperConfigurationExpression MapperConfiguration { get; }

    public IServiceProvider ServiceProvider { get; }

    public SmartSoftwareAutoMapperConfigurationContext(
        IMapperConfigurationExpression mapperConfigurationExpression,
        IServiceProvider serviceProvider)
    {
        MapperConfiguration = mapperConfigurationExpression;
        ServiceProvider = serviceProvider;
    }
}
