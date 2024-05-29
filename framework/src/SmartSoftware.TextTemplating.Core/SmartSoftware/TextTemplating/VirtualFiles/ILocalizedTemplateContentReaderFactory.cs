using System.Threading.Tasks;

namespace SmartSoftware.TextTemplating.VirtualFiles;

public interface ILocalizedTemplateContentReaderFactory
{
    Task<ILocalizedTemplateContentReader> CreateAsync(TemplateDefinition templateDefinition);
}
