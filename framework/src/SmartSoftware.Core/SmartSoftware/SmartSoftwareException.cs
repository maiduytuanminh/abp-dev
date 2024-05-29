using System;

namespace SmartSoftware;

/// <summary>
/// Base exception type for those are thrown by SmartSoftware system for SmartSoftware specific exceptions.
/// </summary>
public class SmartSoftwareException : Exception
{
    public SmartSoftwareException()
    {

    }

    public SmartSoftwareException(string? message)
        : base(message)
    {

    }

    public SmartSoftwareException(string? message, Exception? innerException)
        : base(message, innerException)
    {

    }
}
