using System;
using System.Threading.Tasks;
using SmartSoftware.Data;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Docs.Documents;
using SmartSoftware.Docs.GitHub.Documents;
using SmartSoftware.Docs.Projects;

namespace SmartSoftware.Docs
{
    public class DocsTestDataBuilder : ITransientDependency
    {
        private readonly DocsTestData _testData;
        private readonly IProjectRepository _projectRepository;
        private readonly IDocumentRepository _documentRepository;

        public DocsTestDataBuilder(
            DocsTestData testData, 
            IProjectRepository projectRepository, 
            IDocumentRepository documentRepository)
        {
            _testData = testData;
            _projectRepository = projectRepository;
            _documentRepository = documentRepository;
        }

        public async Task BuildAsync()
        {
            var project = new Project(
                _testData.ProjectId,
                "SS vNext",
                "SS",
                GithubDocumentSource.Type,
                "md",
                "index",
                "docs-nav.json",
                "docs-params.json"
            );

            project
                .SetProperty("GitHubRootUrl", "https://github.com/ssframework/ss/tree/{version}/docs/en/")
                .SetProperty("GitHubAccessToken", "123456")
                .SetProperty("GitHubUserAgent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

            await _projectRepository.InsertAsync(project);

            await _documentRepository.InsertAsync(new Document(Guid.NewGuid(), project.Id, "CLI.md", "2.0.0", "en", "CLI.md",
                "this is ss cli", "md", "https://github.com/ssframework/ss/blob/2.0.0/docs/en/CLI.md",
                "https://github.com/ssframework/ss/tree/2.0.0/docs/",
                "https://raw.githubusercontent.com/ssframework/ss/2.0.0/docs/en/", "", DateTime.Now, DateTime.Now,
                DateTime.Now));
        }
    }
}