using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Docs.HtmlConverting
{
    public interface IDocumentSectionRenderer: ITransientDependency
    {
        Task<string> RenderAsync(string doucment, DocumentRenderParameters parameters = null, List<DocumentPartialTemplateContent> partialTemplates = null);

        Task<Dictionary<string, List<string>>> GetAvailableParametersAsync(string document);

        Task<List<DocumentPartialTemplateWithValuesDto>> GetPartialTemplatesInDocumentAsync(string documentContent);
        
        Task<DocumentNavigationsDto> GetDocumentNavigationsAsync(string documentContent);
    }
}
