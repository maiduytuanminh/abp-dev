using System;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.Blogging.Blogs.Dtos
{
    public class BlogDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }
        
        public string ConcurrencyStamp { get; set; }
    }
}
