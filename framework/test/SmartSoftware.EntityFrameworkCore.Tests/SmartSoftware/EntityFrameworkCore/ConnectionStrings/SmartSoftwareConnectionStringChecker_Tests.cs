using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Data;
using Xunit;

namespace SmartSoftware.EntityFrameworkCore.ConnectionStrings;

public class SmartSoftwareConnectionStringChecker_Tests : EntityFrameworkCoreTestBase
{
    [Fact]
    public async Task IsValidAsync()
    {
        var connectionStringChecker = GetRequiredService<IConnectionStringChecker>();
        var result = await connectionStringChecker.CheckAsync(@"Data Source=:memory:");
        result.Connected.ShouldBeTrue();
        result.DatabaseExists.ShouldBeTrue();
    }
}
