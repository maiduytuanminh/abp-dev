using System;
using SmartSoftware.Application.Dtos;

namespace SmartSoftware.CmsKit.Admin.MediaDescriptors;

[Serializable]
public class MediaDescriptorDto : ExtensibleEntityDto<Guid>
{
    public string Name { get; set; }

    public string MimeType { get; set; }

    public int Size { get; set; }
}
