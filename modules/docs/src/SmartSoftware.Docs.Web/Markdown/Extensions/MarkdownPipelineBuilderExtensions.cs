using Markdig;

namespace SmartSoftware.Docs.Markdown.Extensions
{
    public static class MarkdownPipelineBuilderExtensions
    {
        public static MarkdownPipelineBuilder UseHighlightedCodeBlocks(this MarkdownPipelineBuilder pipeline)
        {
            pipeline.Extensions.AddIfNotAlready<HighlightedCodeBlockExtension>();
            return pipeline;
        }
    }
}