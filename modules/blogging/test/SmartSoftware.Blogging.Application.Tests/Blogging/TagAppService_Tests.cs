using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Blogging.Tagging;
using SmartSoftware.Blogging.Tagging.Dtos;
using Xunit;

namespace SmartSoftware.Blogging
{
    public class TagAppService_Tests : BloggingApplicationTestBase
    {
        private readonly ITagAppService _tagAppService;
        private readonly ITagRepository _tagRepository;
        private readonly BloggingTestData _bloggingTestData;

        public TagAppService_Tests()
        {
            _tagAppService = GetRequiredService<ITagAppService>();
            _tagRepository = GetRequiredService<ITagRepository>();
            _bloggingTestData = GetRequiredService<BloggingTestData>();
        }

        [Fact]
        public async Task Should_Get_Popular_Tags()
        {
            var tags = await _tagAppService.GetPopularTagsAsync(_bloggingTestData.Blog1Id, new GetPopularTagsInput() {ResultCount = 5, MinimumPostCount = 0 });

            tags.Count.ShouldBeGreaterThan(0);
        }
    }
}
