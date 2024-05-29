using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace SmartSoftware.TextTemplating.Razor;

public class SmartSoftwareCompiledViewProviderOptions
{
    public Dictionary<string, List<PortableExecutableReference>> TemplateReferences { get; }

    public SmartSoftwareCompiledViewProviderOptions()
    {
        TemplateReferences = new Dictionary<string, List<PortableExecutableReference>>();
    }
}
