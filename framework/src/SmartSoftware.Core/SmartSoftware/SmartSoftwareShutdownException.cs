using System;

namespace SmartSoftware;

public class SmartSoftwareShutdownException : SmartSoftwareException
{
    public SmartSoftwareShutdownException()
    {

    }

    public SmartSoftwareShutdownException(string message)
        : base(message)
    {

    }

    public SmartSoftwareShutdownException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
