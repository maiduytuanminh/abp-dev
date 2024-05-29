using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.VirtualFileSystem;

public class SmartSoftwareFileExtensionContentTypeProvider : IContentTypeProvider, ITransientDependency
{
    protected SmartSoftwareAspNetCoreContentOptions Options { get; }

    public SmartSoftwareFileExtensionContentTypeProvider(IOptions<SmartSoftwareAspNetCoreContentOptions> ssAspNetCoreContentOptions)
    {
        Options = ssAspNetCoreContentOptions.Value;
    }

    public bool TryGetContentType(string subpath, out string contentType)
    {
        var extension = GetExtension(subpath);
        if (extension == null)
        {
            contentType = null!;
            return false;
        }

        return Options.ContentTypeMaps.TryGetValue(extension, out contentType!);
    }

    protected virtual string? GetExtension(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return null;
        }

        var index = path.LastIndexOf('.');
        return index < 0 ? null : path.Substring(index);
    }
}
