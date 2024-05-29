using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Localization.Resources.SmartSoftwareUi;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.ExceptionHandling;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Views.Error;
using SmartSoftware.ExceptionHandling;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Controllers;

public class ErrorController : SmartSoftwareController
{
    protected readonly IExceptionToErrorInfoConverter ErrorInfoConverter;
    protected readonly IHttpExceptionStatusCodeFinder StatusCodeFinder;
    protected readonly IStringLocalizer<SmartSoftwareUiResource> Localizer;
    protected readonly SmartSoftwareErrorPageOptions SmartSoftwareErrorPageOptions;
    protected readonly IExceptionNotifier ExceptionNotifier;
    protected readonly SmartSoftwareExceptionHandlingOptions ExceptionHandlingOptions;

    public ErrorController(
        IExceptionToErrorInfoConverter exceptionToErrorInfoConverter,
        IHttpExceptionStatusCodeFinder httpExceptionStatusCodeFinder,
        IOptions<SmartSoftwareErrorPageOptions> ssErrorPageOptions,
        IStringLocalizer<SmartSoftwareUiResource> localizer,
        IExceptionNotifier exceptionNotifier,
        IOptions<SmartSoftwareExceptionHandlingOptions> exceptionHandlingOptions)
    {
        ErrorInfoConverter = exceptionToErrorInfoConverter;
        StatusCodeFinder = httpExceptionStatusCodeFinder;
        Localizer = localizer;
        ExceptionNotifier = exceptionNotifier;
        ExceptionHandlingOptions = exceptionHandlingOptions.Value;
        SmartSoftwareErrorPageOptions = ssErrorPageOptions.Value;
    }

    public virtual async Task<IActionResult> Index(int httpStatusCode)
    {
        var exHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

        var exception = exHandlerFeature != null
            ? exHandlerFeature.Error
            : new Exception(Localizer["UnhandledException"]);

        await ExceptionNotifier.NotifyAsync(new ExceptionNotificationContext(exception));

        var errorInfo = ErrorInfoConverter.Convert(exception, options =>
        {
            options.SendExceptionsDetailsToClients = ExceptionHandlingOptions.SendExceptionsDetailsToClients;
            options.SendStackTraceToClients = ExceptionHandlingOptions.SendStackTraceToClients;
        });

        if (httpStatusCode == 0)
        {
            httpStatusCode = (int)StatusCodeFinder.GetStatusCode(HttpContext, exception);
        }

        HttpContext.Response.StatusCode = httpStatusCode;

        var page = GetErrorPageUrl(httpStatusCode);

        return View(page, new SmartSoftwareErrorViewModel
        {
            ErrorInfo = errorInfo,
            HttpStatusCode = httpStatusCode
        });
    }

    protected virtual string GetErrorPageUrl(int statusCode)
    {
        var page = SmartSoftwareErrorPageOptions.ErrorViewUrls.GetOrDefault(statusCode.ToString());

        if (string.IsNullOrWhiteSpace(page))
        {
            return "~/Views/Error/Default.cshtml";
        }

        return page;
    }
}
