using System;
using SmartSoftware.ExceptionHandling;

namespace SmartSoftware.GlobalFeatures;

[Serializable]
public class SmartSoftwareGlobalFeatureNotEnabledException : SmartSoftwareException, IHasErrorCode
{
    public string? Code { get; }

    public SmartSoftwareGlobalFeatureNotEnabledException(string? message = null, string? code = null, Exception? innerException = null)
        : base(message, innerException)
    {
        Code = code;
    }

    public SmartSoftwareGlobalFeatureNotEnabledException WithData(string name, object value)
    {
        Data[name] = value;
        return this;
    }
}
