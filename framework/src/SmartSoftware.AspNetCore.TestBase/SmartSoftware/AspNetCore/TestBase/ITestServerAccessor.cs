using Microsoft.AspNetCore.TestHost;

namespace SmartSoftware.AspNetCore.TestBase;

public interface ITestServerAccessor
{
    TestServer Server { get; set; }
}
