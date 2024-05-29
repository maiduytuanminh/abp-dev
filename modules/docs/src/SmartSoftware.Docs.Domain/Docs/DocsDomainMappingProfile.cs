using AutoMapper;
using SmartSoftware.Docs.Documents;
using SmartSoftware.Docs.Projects;

namespace SmartSoftware.Docs
{
    public class DocsDomainMappingProfile : Profile
    {
        public DocsDomainMappingProfile()
        {
            CreateMap<Document, DocumentEto>();
            CreateMap<Project, ProjectEto>();
        }
    }
}