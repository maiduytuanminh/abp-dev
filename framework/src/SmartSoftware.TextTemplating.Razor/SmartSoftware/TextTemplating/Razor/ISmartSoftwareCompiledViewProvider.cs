using System.Reflection;
using System.Threading.Tasks;

namespace SmartSoftware.TextTemplating.Razor;

public interface ISmartSoftwareCompiledViewProvider
{
    Task<Assembly> GetAssemblyAsync(TemplateDefinition templateDefinition);
}
