namespace SmartSoftware.AspNetCore.ExceptionHandling;

public class SmartSoftwareExceptionHandlingOptions
{
    public bool SendExceptionsDetailsToClients { get; set; } = false;

    public bool SendStackTraceToClients { get; set; } = true;
}
