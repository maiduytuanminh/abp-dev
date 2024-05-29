﻿using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.Reflection;

namespace SmartSoftware.ObjectMapping;

public class SmartSoftwareObjectMappingModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnExposing(onServiceExposingContext =>
        {
            //Register types for IObjectMapper<TSource, TDestination> if implements
            onServiceExposingContext.ExposedTypes.AddRange(
                ReflectionHelper.GetImplementedGenericTypes(
                    onServiceExposingContext.ImplementationType,
                    typeof(IObjectMapper<,>)
                ).ConvertAll(t => new ServiceIdentifier(t))
            );
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(
            typeof(IObjectMapper<>),
            typeof(DefaultObjectMapper<>)
        );
    }
}
