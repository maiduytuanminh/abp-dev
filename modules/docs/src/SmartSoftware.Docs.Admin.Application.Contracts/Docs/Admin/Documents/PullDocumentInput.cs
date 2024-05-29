using System;
using System.ComponentModel.DataAnnotations;
using SmartSoftware.Validation;
using SmartSoftware.Docs.Documents;

namespace SmartSoftware.Docs.Admin.Documents
{
    public class PullDocumentInput : PullAllDocumentInput
    {
        [DynamicStringLength(typeof(DocumentConsts), nameof(DocumentConsts.MaxNameLength))]
        public string Name { get; set; }
    }
}
