﻿using System.Security.Claims;
using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.AspNetCore.Mvc.Authorization;
using Xunit;

namespace SmartSoftware.AspNetCore.Mvc.Security.Claims;

public class ClaimsMapTestController_Tests : AspNetCoreMvcTestBase
{
    private readonly FakeUserClaims _fakeRequiredService;

    public ClaimsMapTestController_Tests()
    {
        _fakeRequiredService = GetRequiredService<FakeUserClaims>();
    }

    [Fact]
    public async Task Claims_Should_Be_Mapped()
    {
        _fakeRequiredService.Claims.AddRange(new[]
        {
                new Claim("SerialNumber", "123456"),
                new Claim("DateOfBirth", "2020")
            });

        var result = await GetResponseAsStringAsync("/ClaimsMapTest/ClaimsMapTest");
        result.ShouldBe("OK");
    }
}
