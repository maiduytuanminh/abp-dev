﻿using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using Shouldly;
using SmartSoftware.AspNetCore.ExceptionHandling;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.Security.Claims;
using Xunit;

namespace SmartSoftware.AspNetCore.Mvc.ExceptionHandling;

public class SmartSoftwareAuthorizationExceptionTestPage_Tests : AspNetCoreMvcTestBase
{
    private IExceptionSubscriber _fakeExceptionSubscriber;

    private FakeUserClaims _fakeRequiredService;

    public SmartSoftwareAuthorizationExceptionTestPage_Tests()
    {
        _fakeRequiredService = GetRequiredService<FakeUserClaims>();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        _fakeExceptionSubscriber = Substitute.For<IExceptionSubscriber>();

        services.AddSingleton(_fakeExceptionSubscriber);

        services.Configure<SmartSoftwareAuthorizationExceptionHandlerOptions>(options =>
        {
            options.AuthenticationScheme = "Cookie";
        });
    }

    [Fact]
    public virtual async Task Should_Handle_By_Cookie_AuthenticationScheme_For_SmartSoftwareAuthorizationException()
    {
        var result = await GetResponseAsync("/ExceptionHandling/ExceptionTestPage?handler=SmartSoftwareAuthorizationException", HttpStatusCode.Redirect);
        result.Headers.Location.ToString().ShouldContain("http://localhost/Account/Login");

#pragma warning disable 4014
        _fakeExceptionSubscriber
            .Received()
            .HandleAsync(Arg.Any<ExceptionNotificationContext>());
#pragma warning restore 4014


        _fakeRequiredService.Claims.AddRange(new[]
        {
                new Claim(SmartSoftwareClaimTypes.UserId, Guid.NewGuid().ToString())
            });

        result = await GetResponseAsync("/ExceptionHandling/ExceptionTestPage?handler=SmartSoftwareAuthorizationException", HttpStatusCode.Redirect);
        result.Headers.Location.ToString().ShouldContain("http://localhost/Account/AccessDenied");

#pragma warning disable 4014
        _fakeExceptionSubscriber
            .Received()
            .HandleAsync(Arg.Any<ExceptionNotificationContext>());
#pragma warning restore 4014
    }
}
