using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace SmartSoftware.AspNetCore.Mvc.Security.Claims;

public class ClaimsMapTestController : SmartSoftwareController
{
    public ActionResult ClaimsMapTest()
    {
        var serialNumber = CurrentUser.FindClaim(ClaimTypes.SerialNumber);
        serialNumber.ShouldNotBeNull();
        serialNumber.Value.ShouldBe("123456");

        var dateOfBirth = CurrentUser.FindClaim(ClaimTypes.DateOfBirth);
        dateOfBirth.ShouldNotBeNull();
        dateOfBirth.Value.ShouldBe("2020");

        return Content("OK");
    }
}
