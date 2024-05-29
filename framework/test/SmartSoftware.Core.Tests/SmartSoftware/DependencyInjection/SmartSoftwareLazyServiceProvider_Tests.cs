using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.Modularity;
using SmartSoftware.Testing.Utils;
using Xunit;

namespace SmartSoftware.DependencyInjection;

public class SmartSoftwareLazyServiceProvider_Tests
{
    [Fact]
    public void LazyServiceProvider_Should_Cache_Services()
    {
        using (var application = SmartSoftwareApplicationFactory.Create<TestModule>())
        {
            application.Initialize();

            var lazyServiceProvider = application.ServiceProvider.GetRequiredService<ISmartSoftwareLazyServiceProvider>();

            var transientTestService1 = lazyServiceProvider.LazyGetRequiredService<TransientTestService>();
            var transientTestService2 = lazyServiceProvider.LazyGetRequiredService<TransientTestService>();
            transientTestService1.ShouldBeSameAs(transientTestService2);

            var testCounter = application.ServiceProvider.GetRequiredService<ITestCounter>();
            testCounter.GetValue(nameof(TransientTestService)).ShouldBe(1);
        }
    }

    [DependsOn(typeof(SmartSoftwareTestBaseModule))]
    private class TestModule : SmartSoftwareModule
    {
        public TestModule()
        {
            SkipAutoServiceRegistration = true;
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddType<TransientTestService>();
        }
    }

    private class TransientTestService : ITransientDependency
    {
        public TransientTestService(ITestCounter counter)
        {
            counter.Increment(nameof(TransientTestService));
        }
    }
}
