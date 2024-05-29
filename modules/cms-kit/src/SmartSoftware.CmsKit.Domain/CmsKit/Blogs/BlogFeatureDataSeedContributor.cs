using System.Threading.Tasks;
using SmartSoftware.Data;
using SmartSoftware.DependencyInjection;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.GlobalFeatures;

namespace SmartSoftware.CmsKit.Blogs;

public class BlogFeatureDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly BlogFeatureManager _blogFeatureManager;
    private readonly IBlogRepository _blogRepository;

    public BlogFeatureDataSeedContributor(
        BlogFeatureManager blogFeatureManager,
        IBlogRepository blogRepository)
    {
        _blogFeatureManager = blogFeatureManager;
        _blogRepository = blogRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (!GlobalFeatureManager.Instance.IsEnabled<BlogsFeature>())
        {
            return;
        }

        var blogs = await _blogRepository.GetListAsync();

        foreach (var blog in blogs)
        {
            await _blogFeatureManager.SetDefaultsIfNotSetAsync(blog.Id);
        }
    }
}
