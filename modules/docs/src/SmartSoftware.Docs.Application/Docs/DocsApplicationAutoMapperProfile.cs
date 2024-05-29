using AutoMapper;
using SmartSoftware.Docs.Documents;
using SmartSoftware.Docs.Projects;
using SmartSoftware.AutoMapper;

namespace SmartSoftware.Docs
{
    public class DocsApplicationAutoMapperProfile : Profile
    {
        public DocsApplicationAutoMapperProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<VersionInfo, VersionInfoDto>();
            CreateMap<Document, DocumentWithDetailsDto>().Ignore(x => x.Project).Ignore(x => x.Contributors);
            CreateMap<DocumentContributor, DocumentContributorDto>();
            CreateMap<DocumentResource, DocumentResourceDto>();
        }
    }
}
