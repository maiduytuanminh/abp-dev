using System;

namespace SmartSoftware.Security.Claims;

[Serializable]
public class SmartSoftwareDynamicClaim
{
    public string Type { get; set; }

    public string? Value { get; set; }

    public SmartSoftwareDynamicClaim(string type, string? value)
    {
        Type = type;
        Value = value;
    }
}
