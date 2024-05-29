using System;
using System.Collections.Generic;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.Docs.Admin.Projects
{
    [Serializable]
    public class ProjectDto : EntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Format { get; set; }

        public string DefaultDocumentName { get; set; }

        public string NavigationDocumentName { get; set; }

        public string ParametersDocumentName { get; set; }

        public string MinimumVersion { get; set; }

        public string MainWebsiteUrl { get; set; }

        public string LatestVersionBranchName { get; set; }

        public string DocumentStoreType { get; set; }

        public Dictionary<string, object> ExtraProperties { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
