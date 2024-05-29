﻿using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shouldly;
using SmartSoftware.Modularity;
using SmartSoftware.Testing;
using Xunit;
using IObjectMapper = SmartSoftware.ObjectMapping.IObjectMapper;

namespace SmartSoftware.AutoMapper;

public class AutoMapper_CustomServiceConstruction_Tests : SmartSoftwareIntegratedTest<AutoMapper_CustomServiceConstruction_Tests.TestModule>
{
    private readonly IObjectMapper _objectMapper;

    public AutoMapper_CustomServiceConstruction_Tests()
    {
        _objectMapper = ServiceProvider.GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public void Should_Custom_Service_Construction()
    {
        var source = new SourceModel
        {
            Name = nameof(SourceModel)
        };

        _objectMapper.Map<SourceModel, DestModel>(source).Name.ShouldBe(nameof(CustomMappingAction));
    }

    [DependsOn(typeof(SmartSoftwareAutoMapperModule))]
    public class TestModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // Replace the build-in IMapper with a custom one to use ConstructServicesUsing.
            context.Services.Replace(ServiceDescriptor.Transient<IMapper>(sp => sp.GetRequiredService<IConfigurationProvider>().CreateMapper()));

            Configure<SmartSoftwareAutoMapperOptions>(options =>
            {
                options.AddMaps<TestModule>();
                options.Configurators.Add(configurationContext =>
                {
                    configurationContext.MapperConfiguration.ConstructServicesUsing(type =>
                        type.Name.Contains(nameof(CustomMappingAction))
                            ? new CustomMappingAction(nameof(CustomMappingAction))
                            : Activator.CreateInstance(type));
                });
            });
        }
    }

    public class SourceModel
    {
        public string Name { get; set; }
    }

    public class DestModel
    {
        public string Name { get; set; }
    }

    public class MapperActionProfile : Profile
    {
        public MapperActionProfile()
        {
            CreateMap<SourceModel, DestModel>().AfterMap<CustomMappingAction>();
        }
    }

    public class CustomMappingAction : IMappingAction<SourceModel, DestModel>
    {
        private readonly string _name;

        public CustomMappingAction(string name)
        {
            _name = name;
        }

        public void Process(SourceModel source, DestModel destination, ResolutionContext context)
        {
            destination.Name = _name;
        }
    }
}
