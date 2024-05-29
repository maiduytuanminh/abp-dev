using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Blogging.Tagging;
using SmartSoftware.Blogging.Tagging.Dtos;

namespace SmartSoftware.Blogging
{
    [RemoteService(Name = BloggingRemoteServiceConsts.RemoteServiceName)]
    [Area(BloggingRemoteServiceConsts.ModuleName)]
    [Route("api/blogging/tags")]
    public class TagsController : SmartSoftwareControllerBase, ITagAppService
    {
        private readonly ITagAppService _tagAppService;

        public TagsController(ITagAppService tagAppService)
        {
            _tagAppService = tagAppService;
        }

        [HttpGet]
        [Route("popular/{blogId}")]
        public Task<List<TagDto>> GetPopularTagsAsync(Guid blogId, GetPopularTagsInput input)
        {
            return _tagAppService.GetPopularTagsAsync(blogId, input);
        }
    }
}
