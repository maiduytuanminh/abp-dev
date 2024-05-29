using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.Modularity;
using SmartSoftware.Testing;
using Xunit;
using IObjectMapper = SmartSoftware.ObjectMapping.IObjectMapper;

namespace SmartSoftware.AutoMapper;

public class AutoMapper_ConfigurationValidation_Tests : SmartSoftwareIntegratedTest<AutoMapper_ConfigurationValidation_Tests.TestModule>
{
    private readonly IObjectMapper _objectMapper;

    public AutoMapper_ConfigurationValidation_Tests()
    {
        _objectMapper = ServiceProvider.GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public void Should_Validate_Configuration()
    {
        _objectMapper.Map<MySourceClass, MyClassValidated>(new MySourceClass { Value = "42" }).Value.ShouldBe("42");
        _objectMapper.Map<MySourceClass, MyClassNonValidated>(new MySourceClass { Value = "42" }).ValueNotMatched.ShouldBe(null);
    }

    [DependsOn(typeof(SmartSoftwareAutoMapperModule))]
    public class TestModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<SmartSoftwareAutoMapperOptions>(options =>
            {
                options.AddMaps<TestModule>(validate: true); //Adds all profiles in the TestModule assembly by validating configurations
                    options.ValidateProfile<NonValidatedProfile>(validate: false); //Exclude a profile from the configuration validation
                });
        }
    }

    public class ValidatedProfile : Profile
    {
        public ValidatedProfile()
        {
            CreateMap<MySourceClass, MyClassValidated>();
        }
    }

    public class NonValidatedProfile : Profile
    {
        public NonValidatedProfile()
        {
            CreateMap<MySourceClass, MyClassNonValidated>();
        }
    }

    public class MySourceClass
    {
        public string Value { get; set; }
    }

    public class MyClassValidated
    {
        public string Value { get; set; }
    }

    public class MyClassNonValidated
    {
        public string ValueNotMatched { get; set; }
    }
}
