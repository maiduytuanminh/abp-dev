using Shouldly;
using SmartSoftware.AspNetCore.ExceptionHandling;
using SmartSoftware.Authorization;
using SmartSoftware.Localization;
using Xunit;

namespace Microsoft.AspNetCore.Authorization;

public class SmartSoftwareAuthorizationServiceExtensions_Tests : AuthorizationTestBase
{
    private readonly IExceptionToErrorInfoConverter _exceptionToErrorInfoConverter;

    public SmartSoftwareAuthorizationServiceExtensions_Tests()
    {
        _exceptionToErrorInfoConverter = GetRequiredService<IExceptionToErrorInfoConverter>();
    }

    [Fact]
    public void Test_SmartSoftwareAuthorizationException_Localization()
    {
        using (CultureHelper.Use("zh-Hans"))
        {
            var exception = new SmartSoftwareAuthorizationException(code: SmartSoftwareAuthorizationErrorCodes.GivenPolicyHasNotGranted);
            var errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("授权失败！提供的策略尚未授予。");

            exception = new SmartSoftwareAuthorizationException(code: SmartSoftwareAuthorizationErrorCodes.GivenPolicyHasNotGrantedWithPolicyName)
                .WithData("PolicyName", "my_policy_name");
            errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("授权失败！提供的策略尚未授予： my_policy_name");

            exception = new SmartSoftwareAuthorizationException(code: SmartSoftwareAuthorizationErrorCodes.GivenPolicyHasNotGrantedForGivenResource)
                .WithData("ResourceName", "my_resource_name");
            errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("授权失败！提供的策略未授予提供的资源：my_resource_name");

            exception = new SmartSoftwareAuthorizationException(code: SmartSoftwareAuthorizationErrorCodes.GivenRequirementHasNotGrantedForGivenResource)
                .WithData("ResourceName", "my_resource_name");
            errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("授权失败！提供的要求未授予提供的资源：my_resource_name");

            exception = new SmartSoftwareAuthorizationException(code: SmartSoftwareAuthorizationErrorCodes.GivenRequirementsHasNotGrantedForGivenResource)
                .WithData("ResourceName", "my_resource_name");
            errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("授权失败！提供的要求未授予提供的资源：my_resource_name");
        }
    }
}
