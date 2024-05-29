using System.Text;
using Markdig;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Docs.Markdown.Extensions;

namespace SmartSoftware.Docs.Markdown
{
    public class MarkDigMarkdownConverter : IMarkdownConverter, ISingletonDependency
    {
        readonly MarkdownPipeline _markdownPipeline;

        public MarkDigMarkdownConverter()
        {
            _markdownPipeline = new MarkdownPipelineBuilder()
              .UseAutoLinks()
              .UseBootstrap()
              .UseGridTables()
              .UsePipeTables()
              .UseHighlightedCodeBlocks()
              .Build();
        }

        public virtual string ConvertToHtml(string markdown)
        {
            return Markdig.Markdown.ToHtml(Encoding.UTF8.GetString(Encoding.Default.GetBytes(markdown)),
                _markdownPipeline);
        }
    }
}