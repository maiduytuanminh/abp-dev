using System;
using Microsoft.Extensions.Logging;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.Logging;

namespace SmartSoftware.Authorization;

/// <summary>
/// This exception is thrown on an unauthorized request.
/// </summary>
public class SmartSoftwareAuthorizationException : SmartSoftwareException, IHasLogLevel, IHasErrorCode
{
    /// <summary>
    /// Severity of the exception.
    /// Default: Warn.
    /// </summary>
    public LogLevel LogLevel { get; set; }

    /// <summary>
    /// Error code.
    /// </summary>
    public string? Code { get; }

    /// <summary>
    /// Creates a new <see cref="SmartSoftwareAuthorizationException"/> object.
    /// </summary>
    public SmartSoftwareAuthorizationException()
    {
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Creates a new <see cref="SmartSoftwareAuthorizationException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public SmartSoftwareAuthorizationException(string message)
        : base(message)
    {
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Creates a new <see cref="SmartSoftwareAuthorizationException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public SmartSoftwareAuthorizationException(string message, Exception innerException)
        : base(message, innerException)
    {
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Creates a new <see cref="SmartSoftwareAuthorizationException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="code">Exception code</param>
    /// <param name="innerException">Inner exception</param>
    public SmartSoftwareAuthorizationException(string? message = null, string? code = null, Exception? innerException = null)
        : base(message, innerException)
    {
        Code = code;
        LogLevel = LogLevel.Warning;
    }

    public SmartSoftwareAuthorizationException WithData(string name, object value)
    {
        Data[name] = value;
        return this;
    }
}
