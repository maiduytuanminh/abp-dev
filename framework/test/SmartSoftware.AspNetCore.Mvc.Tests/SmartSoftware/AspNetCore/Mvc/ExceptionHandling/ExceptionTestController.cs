using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.Authorization;

namespace SmartSoftware.AspNetCore.Mvc.ExceptionHandling;

[Route("api/exception-test")]
public class ExceptionTestController : SmartSoftwareController
{
    [HttpGet]
    [Route("UserFriendlyException1")]
    public void UserFriendlyException1()
    {
        throw new UserFriendlyException("This is a sample exception!");
    }

    [HttpGet]
    [Route("UserFriendlyException2")]
    public ActionResult UserFriendlyException2()
    {
        throw new UserFriendlyException("This is a sample exception!");
    }

    [HttpGet]
    [Route("SmartSoftwareAuthorizationException")]
    public void SmartSoftwareAuthorizationException()
    {
        throw new SmartSoftwareAuthorizationException("This is a sample exception!");
    }

    [HttpGet]
    [Route("ExceptionOnUowSaveChange")]
    public Task<string> ExceptionOnUowSaveChangeAsync()
    {
        return Task.FromResult("OK");
    }
}
