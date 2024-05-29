using System;

namespace SmartSoftware.Data;

public class SmartSoftwareDbConcurrencyException : SmartSoftwareException
{
    /// <summary>
    /// Creates a new <see cref="SmartSoftwareDbConcurrencyException"/> object.
    /// </summary>
    public SmartSoftwareDbConcurrencyException()
    {

    }

    /// <summary>
    /// Creates a new <see cref="SmartSoftwareDbConcurrencyException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public SmartSoftwareDbConcurrencyException(string message)
        : base(message)
    {

    }

    /// <summary>
    /// Creates a new <see cref="SmartSoftwareDbConcurrencyException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public SmartSoftwareDbConcurrencyException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
