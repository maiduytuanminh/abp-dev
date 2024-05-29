using JetBrains.Annotations;
using SmartSoftware;

namespace SmartSoftware.CmsKit.MediaDescriptors;

public class InvalidMediaDescriptorNameException : BusinessException
{
    public InvalidMediaDescriptorNameException([NotNull] string name)
    {
        Code = CmsKitErrorCodes.MediaDescriptors.InvalidName;
        WithData(nameof(MediaDescriptor.Name), name);
    }
}
