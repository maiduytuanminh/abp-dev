using System;

namespace SmartSoftware.BlobStoring;

public class BlobAlreadyExistsException : SmartSoftwareException
{
    public BlobAlreadyExistsException()
    {

    }

    public BlobAlreadyExistsException(string message)
        : base(message)
    {

    }

    public BlobAlreadyExistsException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
