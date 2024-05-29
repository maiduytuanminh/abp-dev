using System.ComponentModel.DataAnnotations;
using SmartSoftware.Content;
using SmartSoftware.Validation;
using SmartSoftware.CmsKit.MediaDescriptors;

namespace SmartSoftware.CmsKit.Admin.MediaDescriptors;

public class CreateMediaInputWithStream
{
    [Required]
    [DynamicStringLength(typeof(MediaDescriptorConsts), nameof(MediaDescriptorConsts.MaxNameLength))]
    public string Name { get; set; }

    public IRemoteStreamContent File { get; set; }
}
