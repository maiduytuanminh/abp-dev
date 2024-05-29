using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace SmartSoftware.TextTemplating.Razor;

public class SmartSoftwareRazorTemplateCSharpCompilerOptions
{
    public List<PortableExecutableReference> References { get; }

    public SmartSoftwareRazorTemplateCSharpCompilerOptions()
    {
        References = new List<PortableExecutableReference>();
    }
}
