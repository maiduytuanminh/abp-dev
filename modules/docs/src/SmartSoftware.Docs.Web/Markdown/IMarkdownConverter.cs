namespace SmartSoftware.Docs.Markdown
{
    public interface IMarkdownConverter
    {
        string ConvertToHtml(string markdown);
    }
}