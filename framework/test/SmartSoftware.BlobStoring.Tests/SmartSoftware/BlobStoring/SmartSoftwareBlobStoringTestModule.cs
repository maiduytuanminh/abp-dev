using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SmartSoftware.Autofac;
using SmartSoftware.BlobStoring.Fakes;
using SmartSoftware.BlobStoring.TestObjects;
using SmartSoftware.Modularity;

namespace SmartSoftware.BlobStoring;

[DependsOn(
    typeof(SmartSoftwareBlobStoringModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAutofacModule)
    )]
public class SmartSoftwareBlobStoringTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IBlobProvider>(Substitute.For<FakeBlobProvider1>());
        context.Services.AddSingleton<IBlobProvider>(Substitute.For<FakeBlobProvider2>());

        Configure<SmartSoftwareBlobStoringOptions>(options =>
        {
            options.Containers
                .ConfigureDefault(container =>
                {
                    container.SetConfiguration("TestConfigDefault", "TestValueDefault");
                    container.ProviderType = typeof(FakeBlobProvider1);
                })
                .Configure<TestContainer1>(container =>
                {
                    container.SetConfiguration("TestConfig1", "TestValue1");
                    container.ProviderType = typeof(FakeBlobProvider1);
                })
                .Configure<TestContainer2>(container =>
                {
                    container.SetConfiguration("TestConfig2", "TestValue2");
                    container.ProviderType = typeof(FakeBlobProvider2);
                });
        });
    }
}
