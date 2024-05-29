using System;

namespace SmartSoftware.Emailing;

[Serializable]
public class EmailAttachment
{
    public string? Name { get; set; }

    public byte[]? File { get; set; }
}
