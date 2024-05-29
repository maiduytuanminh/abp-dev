using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Docs.Projects;
using Xunit;

namespace SmartSoftware.Docs
{
    public class ApplicationService_Tests : DocsApplicationTestBase
    {
        private readonly IProjectAppService _projectAppService;
        private readonly DocsTestData _testData;

        public ApplicationService_Tests()
        {
            _projectAppService = GetRequiredService<IProjectAppService>();
            _testData = GetRequiredService<DocsTestData>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            var projects = await _projectAppService.GetListAsync();
            projects.ShouldNotBeNull();
            projects.Items.Count.ShouldBe(1);
            projects.Items.ShouldContain(x => x.Id == _testData.ProjectId);
        }

        [Fact]
        public async Task GetAsync()
        {
            var project = await _projectAppService.GetAsync("ss");
            project.ShouldNotBeNull();
            project.ShortName.ShouldBe("ss");
        }

        [Fact]
        public async Task GetVersionsAsync()
        {
            var versions = await _projectAppService.GetVersionsAsync("SS");
            versions.ShouldNotBeNull();
            versions.Items.Count.ShouldBe(1);
            versions.Items.ShouldContain(x => x.Name == "0.15.0" && x.DisplayName == "0.15.0");
        }
    }
}
