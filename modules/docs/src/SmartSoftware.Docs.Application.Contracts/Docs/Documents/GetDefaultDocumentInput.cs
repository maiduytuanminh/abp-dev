using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using SmartSoftware.Validation;
using SmartSoftware.Docs.Language;
using SmartSoftware.Docs.Projects;

namespace SmartSoftware.Docs.Documents
{
    public class GetDefaultDocumentInput
    {
        public Guid ProjectId { get; set; }

        [DynamicStringLength(typeof(ProjectConsts), nameof(ProjectConsts.MaxVersionNameLength))]
        public string Version { get; set; }

        [Required]
        [DynamicStringLength(typeof(LanguageConsts), nameof(LanguageConsts.MaxLanguageCodeLength))]
        public string LanguageCode { get; set; }
    }
}