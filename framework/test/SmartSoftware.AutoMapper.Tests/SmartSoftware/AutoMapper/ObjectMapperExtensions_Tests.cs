using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using SmartSoftware.AutoMapper.SampleClasses;
using SmartSoftware.ObjectMapping;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.AutoMapper;

public class ObjectMapperExtensions_Tests : SmartSoftwareIntegratedTest<AutoMapperTestModule>
{
    private readonly IObjectMapper _objectMapper;

    public ObjectMapperExtensions_Tests()
    {
        _objectMapper = ServiceProvider.GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public void Should_Map_Objects_With_AutoMap_Attributes()
    {
        var dto = _objectMapper.Map<MyEntity, MyEntityDto>(
            new MyEntity
            {
                Number = 42
            }
        );

        dto.As<MyEntityDto>().Number.ShouldBe(42);
    }
}
