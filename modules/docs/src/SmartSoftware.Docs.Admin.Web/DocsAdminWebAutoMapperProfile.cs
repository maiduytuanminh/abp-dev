using AutoMapper;
using SmartSoftware.AutoMapper;
using SmartSoftware.Docs.Admin.Documents;
using SmartSoftware.Docs.Admin.Pages.Docs.Admin.Projects;
using SmartSoftware.Docs.Admin.Projects;

namespace SmartSoftware.Docs.Admin
{
    public class DocsAdminWebAutoMapperProfile : Profile
    {
        public DocsAdminWebAutoMapperProfile()
        {
            CreateMap<CreateModel.CreateGithubProjectViewModel, CreateProjectDto>()
                .Ignore(x => x.ExtraProperties);

            CreateMap<EditModel.EditGithubProjectViewModel, UpdateProjectDto>()
                .Ignore(x => x.ExtraProperties);

            CreateMap<ProjectDto, EditModel.EditGithubProjectViewModel > ()
                .Ignore(x => x.GitHubAccessToken)
                .Ignore(x => x.GitHubRootUrl)
                .Ignore(x => x.GitHubUserAgent)
                .Ignore(x => x.GithubVersionProviderSource)
                .Ignore(x => x.VersionBranchPrefix);

            CreateMap<PullModel.PullDocumentViewModel, PullAllDocumentInput>();
            CreateMap<PullModel.PullDocumentViewModel, PullDocumentInput>();
        }
    }
}
