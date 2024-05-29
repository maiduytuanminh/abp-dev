using System;
using SmartSoftware.Application.Dtos;

namespace SmartSoftware.CmsKit.Public.GlobalResources;

[Serializable]
public class GlobalResourceDto : ExtensibleAuditedEntityDto
{
    public string Name { get; set; }

    public string Value { get; set; }
}