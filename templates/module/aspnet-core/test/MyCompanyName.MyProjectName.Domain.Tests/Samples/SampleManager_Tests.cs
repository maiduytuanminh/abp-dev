using System.Threading.Tasks;
using SmartSoftware.Modularity;
using Xunit;

namespace MyCompanyName.MyProjectName.Samples;

public abstract class SampleManager_Tests<TStartupModule> : MyProjectNameDomainTestBase<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    //private readonly SampleManager _sampleManager;

    public SampleManager_Tests()
    {
        //_sampleManager = GetRequiredService<SampleManager>();
    }

    [Fact]
    public Task Method1Async()
    {
        return Task.CompletedTask;
    }
}
