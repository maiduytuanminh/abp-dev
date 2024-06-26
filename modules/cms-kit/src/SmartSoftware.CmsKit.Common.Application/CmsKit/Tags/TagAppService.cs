﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSoftware.CmsKit.Tags;

namespace SmartSoftware.CmsKit.Tags;

public class TagAppService : CmsKitAppServiceBase, ITagAppService
{
    protected ITagRepository TagRepository { get; }

    public TagAppService(ITagRepository tagRepository)
    {
        TagRepository = tagRepository;
    }

    public virtual async Task<List<TagDto>> GetAllRelatedTagsAsync(string entityType, string entityId)
    {
        var entities = await TagRepository.GetAllRelatedTagsAsync(
            entityType,
            entityId);

        return ObjectMapper.Map<List<Tag>, List<TagDto>>(entities);
    }

    public virtual async Task<List<PopularTagDto>> GetPopularTagsAsync(string entityType, int maxCount)
    {
        return ObjectMapper.Map<List<PopularTag>, List<PopularTagDto>>(
            await TagRepository.GetPopularTagsAsync(
                entityType,
                maxCount
            )
        );
    }
}