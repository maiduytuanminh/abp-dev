using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace SmartSoftware.AspNetCore.VirtualFileSystem;

public class VirtualFileSystem_Tests : SmartSoftwareAspNetCoreTestBase
{
    [Fact]
    public async Task Get_Virtual_File()
    {
        var result = await GetResponseAsStringAsync(
            "/SampleFiles/test1.js"
        );

        result.ShouldBe("test1.js-content");
    }
}
