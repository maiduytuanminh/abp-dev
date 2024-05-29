using SmartSoftware.Docs.Documents;
using SmartSoftware.Docs.Projects;

namespace SmartSoftware.Docs.HtmlConverting
{
    public interface IDocumentToHtmlConverter
    {
        string Convert(ProjectDto project, DocumentWithDetailsDto document, string version, string languageCode);
    }
}