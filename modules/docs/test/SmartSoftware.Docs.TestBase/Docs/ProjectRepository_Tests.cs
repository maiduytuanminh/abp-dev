using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Modularity;
using SmartSoftware.Docs.Projects;
using Xunit;

namespace SmartSoftware.Docs
{
    public abstract class ProjectRepository_Tests<TStartupModule> : DocsTestBase<TStartupModule>
        where TStartupModule : ISmartSoftwareModule
    {
        protected readonly IProjectRepository ProjectRepository;

        protected ProjectRepository_Tests()
        {
            ProjectRepository = GetRequiredService<IProjectRepository>(); ;
        }

        [Fact]
        public async Task GetListAsync()
        {
            var projects = await ProjectRepository.GetListAsync();

            projects.Count.ShouldBe(1);
        }
        
        [Fact]
        public async Task GetByShortNameAsync()
        {
            var project = await ProjectRepository.GetByShortNameAsync("ss");

            project.ShouldNotBeNull();
            project.ShortName.ShouldBe("ss");
        }
    }
}
