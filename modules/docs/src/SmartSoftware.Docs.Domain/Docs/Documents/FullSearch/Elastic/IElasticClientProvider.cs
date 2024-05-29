using Nest;

namespace SmartSoftware.Docs.Documents.FullSearch.Elastic
{
    public interface IElasticClientProvider
    {
        IElasticClient GetClient();
    }
}