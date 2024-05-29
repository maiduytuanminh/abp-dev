using System;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;

public interface IExtensionPropertyAttributeDtoFactory
{
    ExtensionPropertyAttributeDto Create(Attribute attribute);
}
