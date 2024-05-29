using System.IO;
using System.Linq;

namespace SmartSoftware.CmsKit.MediaDescriptors;

public static class MediaDescriptorChecks
{
    public static bool IsValidMediaFileName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        return !Path.GetInvalidFileNameChars().Any(name.Contains);
    }
}
