using System;

namespace SmartSoftware.Docs.Documents;

public class DocumentInfo
{
    public string Version { get; set; }
    public string Format { get; set; }
    public string LanguageCode { get; set; }
    public Guid ProjectId { get; set; }
}