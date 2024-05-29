using JetBrains.Annotations;

namespace SmartSoftware.Minify;

public interface IMinifier
{
    string Minify(
        string source,
        string? fileName = null,
        string? originalFileName = null);
}
