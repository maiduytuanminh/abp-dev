using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartSoftware.Blogging.Tagging.Dtos;

namespace SmartSoftware.Blogging.Tagging
{
    public class TagAppService : BloggingAppServiceBase, ITagAppService
    {
        protected ITagRepository TagRepository { get; }

        public TagAppService(ITagRepository tagRepository)
        {
            TagRepository = tagRepository;
        }

        public virtual async Task<List<TagDto>> GetPopularTagsAsync(Guid blogId, GetPopularTagsInput input)
        {
            var postTags = (await TagRepository.GetListAsync(blogId)).OrderByDescending(t=>t.UsageCount)
                .WhereIf(input.MinimumPostCount != null, t=>t.UsageCount >= input.MinimumPostCount)
                .Take(input.ResultCount).ToList();

            return new List<TagDto>(
                ObjectMapper.Map<List<Tag>, List<TagDto>>(postTags));
        }
    }
}
