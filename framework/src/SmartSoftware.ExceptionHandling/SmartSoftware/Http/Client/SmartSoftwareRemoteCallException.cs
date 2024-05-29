using System;
using SmartSoftware.ExceptionHandling;

namespace SmartSoftware.Http.Client;

public class SmartSoftwareRemoteCallException : SmartSoftwareException, IHasErrorCode, IHasErrorDetails, IHasHttpStatusCode
{
    public int HttpStatusCode { get; set; }

    public string? Code => Error?.Code;

    public string? Details => Error?.Details;

    public RemoteServiceErrorInfo? Error { get; set; }

    public SmartSoftwareRemoteCallException()
    {

    }

    public SmartSoftwareRemoteCallException(string message, Exception? innerException = null)
        : base(message, innerException)
    {

    }

    public SmartSoftwareRemoteCallException(RemoteServiceErrorInfo error, Exception? innerException = null)
        : base(error.Message, innerException)
    {
        Error = error;

        if (error.Data != null)
        {
            foreach (var dataKey in error.Data.Keys)
            {
                Data[dataKey] = error.Data[dataKey];
            }
        }
    }
}
