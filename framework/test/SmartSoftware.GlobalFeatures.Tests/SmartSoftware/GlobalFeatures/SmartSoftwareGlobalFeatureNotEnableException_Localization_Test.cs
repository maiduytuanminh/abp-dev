using Shouldly;
using SmartSoftware.AspNetCore.ExceptionHandling;
using SmartSoftware.Localization;
using Xunit;

namespace SmartSoftware.GlobalFeatures;

public class SmartSoftwareGlobalFeatureNotEnableException_Localization_Test : GlobalFeatureTestBase
{
    private readonly IExceptionToErrorInfoConverter _exceptionToErrorInfoConverter;

    public SmartSoftwareGlobalFeatureNotEnableException_Localization_Test()
    {
        _exceptionToErrorInfoConverter = GetRequiredService<IExceptionToErrorInfoConverter>();
    }

    [Fact]
    public void SmartSoftwareAuthorizationException_Localization()
    {
        using (CultureHelper.Use("zh-Hans"))
        {
            var exception = new SmartSoftwareGlobalFeatureNotEnabledException(code: SmartSoftwareGlobalFeatureErrorCodes.GlobalFeatureIsNotEnabled)
                .WithData("ServiceName", "MyService")
                .WithData("GlobalFeatureName", "TestFeature"); ;
            var errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("'MyService' 服务需要启用 'TestFeature'。");
        }
    }
}
