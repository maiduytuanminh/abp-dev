using AutoMapper;
using SmartSoftware.AutoMapper;
using SmartSoftware.Docs.Admin.Documents;
using SmartSoftware.Docs.Admin.Projects;
using SmartSoftware.Docs.Documents;
using SmartSoftware.Docs.Projects;

namespace SmartSoftware.Docs.Admin
{
    public class DocsAdminApplicationAutoMapperProfile : Profile
    {
        public DocsAdminApplicationAutoMapperProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<Document, DocumentDto>().Ignore(x => x.ProjectName);
            CreateMap<DocumentWithoutContent, DocumentDto>();
            CreateMap<ProjectWithoutDetails, ProjectWithoutDetailsDto>();
            CreateMap<DocumentInfo, DocumentInfoDto>();
        }
    }
}