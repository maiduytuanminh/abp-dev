using SmartSoftware;

namespace SmartSoftware.Docs
{
    public class DocumentNotFoundException : BusinessException
    {
        public string DocumentUrl { get; set; }

        public DocumentNotFoundException(string documentUrl)
        {
            DocumentUrl = documentUrl;
        }
    }
}
