using Microsoft.AspNetCore.TestHost;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.TestBase;

public class TestServerAccessor : ITestServerAccessor, ISingletonDependency
{
    public TestServer Server { get; set; } = default!;
}
