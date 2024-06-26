using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using Shouldly;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.Http;
using SmartSoftware.Security.Claims;
using SmartSoftware.Uow;
using Xunit;

namespace SmartSoftware.AspNetCore.Mvc.ExceptionHandling;

public class ExceptionTestPage_Tests : AspNetCoreMvcTestBase
{
    private IExceptionSubscriber _fakeExceptionSubscriber;

    private FakeUserClaims _fakeRequiredService;

    public ExceptionTestPage_Tests()
    {
        _fakeRequiredService = GetRequiredService<FakeUserClaims>();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        _fakeExceptionSubscriber = Substitute.For<IExceptionSubscriber>();

        services.AddSingleton(_fakeExceptionSubscriber);

        services.Replace(ServiceDescriptor.Transient<IUnitOfWork, ExceptionHandingUnitOfWork>());
    }

    [Fact]
    public async Task Should_Not_Handle_Exceptions_For_Void_Return_Values()
    {
        await Assert.ThrowsAsync<UserFriendlyException>(
            async () => await GetResponseAsStringAsync(
                "/ExceptionHandling/ExceptionTestPage?handler=UserFriendlyException_Void"
            )
        );

#pragma warning disable 4014
        _fakeExceptionSubscriber
            .DidNotReceive()
            .HandleAsync(Arg.Any<ExceptionNotificationContext>());
#pragma warning restore 4014
    }

    [Fact]
    public async Task Should_Not_Handle_Exceptions_For_Task_Return_Values()
    {
        await Assert.ThrowsAsync<UserFriendlyException>(
            async () => await GetResponseAsStringAsync(
                "/ExceptionHandling/ExceptionTestPage?handler=UserFriendlyException_Task"
            )
        );

#pragma warning disable 4014
        _fakeExceptionSubscriber
            .DidNotReceive()
            .HandleAsync(Arg.Any<ExceptionNotificationContext>());
#pragma warning restore 4014
    }

    [Fact]
    public async Task Should_Not_Handle_Exceptions_For_ActionResult_Return_Values()
    {
        await Assert.ThrowsAsync<UserFriendlyException>(
            async () => await GetResponseAsStringAsync(
                "/ExceptionHandling/ExceptionTestPage?handler=UserFriendlyException_ActionResult"
            )
        );

#pragma warning disable 4014
        _fakeExceptionSubscriber
            .DidNotReceive()
            .HandleAsync(Arg.Any<ExceptionNotificationContext>());
#pragma warning restore 4014
    }

    [Fact]
    public async Task Should_Return_RemoteServiceErrorResponse_For_UserFriendlyException_For_Object_Return_Value()
    {
        var result = await GetResponseAsObjectAsync<RemoteServiceErrorResponse>("/ExceptionHandling/ExceptionTestPage?handler=UserFriendlyException_JsonResult", HttpStatusCode.Forbidden);
        result.Error.ShouldNotBeNull();
        result.Error.Message.ShouldBe("This is a sample exception!");

#pragma warning disable 4014
        _fakeExceptionSubscriber
            .Received()
            .HandleAsync(Arg.Any<ExceptionNotificationContext>());
#pragma warning restore 4014
    }

    [Fact]
    public async Task Should_Return_RemoteServiceErrorResponse_For_UserFriendlyException_For_Task_Object_Return_Value()
    {
        var result = await GetResponseAsObjectAsync<RemoteServiceErrorResponse>("/ExceptionHandling/ExceptionTestPage?handler=UserFriendlyException_Task_JsonResult", HttpStatusCode.Forbidden);
        result.Error.ShouldNotBeNull();
        result.Error.Message.ShouldBe("This is a sample exception!");

#pragma warning disable 4014
        _fakeExceptionSubscriber
            .Received()
            .HandleAsync(Arg.Any<ExceptionNotificationContext>());
#pragma warning restore 4014
    }


    [Fact]
    public virtual async Task Should_Handle_By_Cookie_AuthenticationScheme_For_SmartSoftwareAuthorizationException_For_Void_Return_Value()
    {
        _fakeRequiredService.Claims.AddRange(new[]
        {
                new Claim(SmartSoftwareClaimTypes.UserId, Guid.NewGuid().ToString())
            });

        var result = await GetResponseAsync("/ExceptionHandling/ExceptionTestPage?handler=SmartSoftwareAuthorizationException", HttpStatusCode.Redirect);
        result.Headers.Location.ToString().ShouldContain("http://localhost/Account/AccessDenied");

#pragma warning disable 4014
        _fakeExceptionSubscriber
            .Received()
            .HandleAsync(Arg.Any<ExceptionNotificationContext>());
#pragma warning restore 4014
    }

    [Fact]
    public virtual async Task Should_Handle_By_JwtBearer_AuthenticationScheme_For_SmartSoftwareAuthorizationException_For_Void_Return_Value()
    {
        var result = await GetResponseAsync("/ExceptionHandling/ExceptionTestPage?handler=SmartSoftwareAuthorizationException", HttpStatusCode.Unauthorized);
        result.Headers.WwwAuthenticate.ToString().ShouldBe("Bearer");

#pragma warning disable 4014
        _fakeExceptionSubscriber
            .Received()
            .HandleAsync(Arg.Any<ExceptionNotificationContext>());
#pragma warning restore 4014
    }


    [Fact]
    public async Task Should_Handle_Exception_On_Uow_SaveChangeAsync()
    {
        _fakeRequiredService.Claims.AddRange(new[]
        {
            new Claim(SmartSoftwareClaimTypes.UserId, Guid.Empty.ToString())
        });

        var result = await GetResponseAsObjectAsync<RemoteServiceErrorResponse>("/api/exception-test/ExceptionOnUowSaveChange", HttpStatusCode.Conflict);
        result.Error.ShouldNotBeNull();
        result.Error.Message.ShouldBe("The data you have submitted has already changed by another user/client. Please discard the changes you've done and try from the beginning.");

#pragma warning disable 4014
        _fakeExceptionSubscriber
            .Received()
            .HandleAsync(Arg.Any<ExceptionNotificationContext>());
#pragma warning restore 4014

    }
}
