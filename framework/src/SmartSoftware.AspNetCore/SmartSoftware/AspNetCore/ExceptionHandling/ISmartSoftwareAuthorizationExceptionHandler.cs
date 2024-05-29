using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartSoftware.Authorization;

namespace SmartSoftware.AspNetCore.ExceptionHandling;

public interface ISmartSoftwareAuthorizationExceptionHandler
{
    Task HandleAsync(SmartSoftwareAuthorizationException exception, HttpContext httpContext);
}
