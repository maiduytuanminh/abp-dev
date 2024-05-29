using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;
using SmartSoftware.CmsKit.Tags;

namespace SmartSoftware.CmsKit.Admin.Tags;

public interface ITagAdminAppService : ICrudAppService<TagDto, Guid, TagGetListInput, TagCreateDto, TagUpdateDto>
{
    Task<List<TagDefinitionDto>> GetTagDefinitionsAsync();
}
