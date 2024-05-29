using System;
using AutoMapper;

namespace SmartSoftware.AutoMapper;

public interface ISmartSoftwareAutoMapperConfigurationContext
{
    IMapperConfigurationExpression MapperConfiguration { get; }

    IServiceProvider ServiceProvider { get; }
}
