using JetBrains.Annotations;

namespace SmartSoftware.TextTemplating.VirtualFiles;

public interface ILocalizedTemplateContentReader
{
    public string? GetContentOrNull(string? culture);
}
