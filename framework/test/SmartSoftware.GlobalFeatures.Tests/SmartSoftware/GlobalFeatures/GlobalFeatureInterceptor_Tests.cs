using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Application.Services;
using SmartSoftware.DependencyInjection;
using Xunit;

namespace SmartSoftware.GlobalFeatures;

public class GlobalFeatureInterceptor_Tests : GlobalFeatureTestBase
{
    private readonly TestAppServiceV1 _testAppServiceV1;
    private readonly TestAppServiceV2 _testAppServiceV2;

    public GlobalFeatureInterceptor_Tests()
    {
        _testAppServiceV1 = GetRequiredService<TestAppServiceV1>();
        _testAppServiceV2 = GetRequiredService<TestAppServiceV2>();
    }

    [Fact]
    public async Task Interceptor_Test()
    {
        var ex = await Assert.ThrowsAsync<SmartSoftwareGlobalFeatureNotEnabledException>(async () =>
        {
            await _testAppServiceV1.TestMethod();
        });

        ex.Data["ServiceName"].ShouldBe(_testAppServiceV1.GetType().FullName);
        ex.Data["GlobalFeatureName"].ShouldBe(TestFeature.Name);

        GlobalFeatureManager.Instance.Enable(TestFeature.Name);

        (await _testAppServiceV1.TestMethod()).ShouldBe(1);
        (await _testAppServiceV2.TestMethod()).ShouldBe(1);
    }

    public interface ITestAppService : IApplicationService
    {
        Task<int> TestMethod();
    }

    [RequiresGlobalFeature(TestFeature.Name)]
    [ExposeServices(typeof(TestAppServiceV1))]
    public class TestAppServiceV1 : ApplicationService, ITestAppService
    {
        public virtual Task<int> TestMethod()
        {
            return Task.FromResult(1);
        }
    }

    [ExposeServices(typeof(TestAppServiceV2))]
    public class TestAppServiceV2 : ApplicationService, ITestAppService
    {
        public virtual Task<int> TestMethod()
        {
            return Task.FromResult(1);
        }
    }
}
