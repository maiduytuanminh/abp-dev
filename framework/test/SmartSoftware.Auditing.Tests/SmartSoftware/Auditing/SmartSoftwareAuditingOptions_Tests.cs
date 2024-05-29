using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace SmartSoftware.Auditing;

public class SmartSoftwareAuditingOptions_Tests : SmartSoftwareAuditingTestBase
{
    private const string ApplicationName = "TEST_APP_NAME";
    
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        base.SetSmartSoftwareApplicationCreationOptions(options);
        options.ApplicationName = ApplicationName;
    }
    
    [Fact]
    public void Should_Set_Application_Name_From_Global_Application_Name_By_Default()
    {
        var options = GetRequiredService<IOptions<SmartSoftwareAuditingOptions>>().Value;
        options.ApplicationName.ShouldBe(ApplicationName);
    }
}