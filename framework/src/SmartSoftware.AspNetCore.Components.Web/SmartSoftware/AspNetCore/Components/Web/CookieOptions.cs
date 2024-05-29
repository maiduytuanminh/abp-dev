using System;

namespace SmartSoftware.AspNetCore.Components.Web;

public class CookieOptions
{
    public DateTimeOffset? ExpireDate { get; set; }
    public string? Path { get; set; }
    public bool Secure { get; set; }
}
