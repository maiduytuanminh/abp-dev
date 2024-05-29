using System;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using SmartSoftware;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Http;
using SmartSoftware.IO;
using SmartSoftware.Docs.Documents;

namespace SmartSoftware.Docs.Areas.Documents
{
    [RemoteService(Name = DocsRemoteServiceConsts.RemoteServiceName)]
    [Area(DocsRemoteServiceConsts.ModuleName)]
    [ControllerName("DocumentResource")]
    [Route("document-resources")]
    public class DocumentResourceController : SmartSoftwareController
    {
        private readonly IDocumentAppService _documentAppService;

        public DocumentResourceController(IDocumentAppService documentAppService)
        {
            _documentAppService = documentAppService;
        }

        [HttpGet]
        [Route("")]
        public virtual async Task<FileResult> GetResource(GetDocumentResourceInput input)
        {
            input.Name = input.Name.RemovePreFix("/");
            var documentResource = await _documentAppService.GetResourceAsync(input);
            var contentType = MimeTypes.GetByExtension(FileHelper.GetExtension(input.Name));
            return File(documentResource.Content, contentType);
        }
    }
}
