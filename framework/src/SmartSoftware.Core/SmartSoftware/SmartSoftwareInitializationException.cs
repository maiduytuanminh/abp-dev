using System;

namespace SmartSoftware;

public class SmartSoftwareInitializationException : SmartSoftwareException
{
    public SmartSoftwareInitializationException()
    {

    }

    public SmartSoftwareInitializationException(string message)
        : base(message)
    {

    }

    public SmartSoftwareInitializationException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
