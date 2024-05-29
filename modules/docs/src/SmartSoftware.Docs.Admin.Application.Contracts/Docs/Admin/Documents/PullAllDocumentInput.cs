using System;
using System.ComponentModel.DataAnnotations;
using SmartSoftware.Validation;
using SmartSoftware.Docs.Documents;

namespace SmartSoftware.Docs.Admin.Documents
{
    public class PullAllDocumentInput
    {
        public Guid ProjectId { get; set; }

        [DynamicStringLength(typeof(DocumentConsts), nameof(DocumentConsts.MaxLanguageCodeNameLength))]
        public string LanguageCode { get; set; }

        [DynamicStringLength(typeof(DocumentConsts), nameof(DocumentConsts.MaxVersionNameLength))]
        public string Version { get; set; }
    }
}