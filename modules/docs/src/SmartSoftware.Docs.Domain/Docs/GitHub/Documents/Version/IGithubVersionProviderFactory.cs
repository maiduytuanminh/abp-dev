using SmartSoftware.Docs.Projects;

namespace SmartSoftware.Docs.GitHub.Documents.Version
{
    public interface IGithubVersionProviderFactory
    {
        IGithubVersionProvider Create(GithubVersionProviderSource source);
    }
}
