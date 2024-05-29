using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Modularity;
using Xunit;

namespace SmartSoftware.TextTemplating.VirtualFiles;

public abstract class VirtualFileTemplateContributor_Tests<TStartupModule> : SmartSoftwareTextTemplatingTestBase<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected readonly ITemplateDefinitionManager TemplateDefinitionManager;
    protected readonly VirtualFileTemplateContentContributor VirtualFileTemplateContentContributor;
    protected string WelcomeEmailEnglishContent;
    protected string WelcomeEmailTurkishContent;
    protected string ForgotPasswordEmailEnglishContent;

    protected VirtualFileTemplateContributor_Tests()
    {
        TemplateDefinitionManager = GetRequiredService<ITemplateDefinitionManager>();
        VirtualFileTemplateContentContributor = GetRequiredService<VirtualFileTemplateContentContributor>();
    }

    [Fact]
    public async Task Should_Get_Localized_Content_By_Culture()
    {
        (await VirtualFileTemplateContentContributor.GetOrNullAsync(
                new TemplateContentContributorContext(await TemplateDefinitionManager.GetAsync(TestTemplates.WelcomeEmail),
                    ServiceProvider,
                    "en")))
            .ShouldBe(WelcomeEmailEnglishContent);

        (await VirtualFileTemplateContentContributor.GetOrNullAsync(
                new TemplateContentContributorContext(await TemplateDefinitionManager.GetAsync(TestTemplates.WelcomeEmail),
                    ServiceProvider,
                    "tr")))
            .ShouldBe(WelcomeEmailTurkishContent);
    }

    [Fact]
    public async Task Should_Get_Non_Localized_Template_Content()
    {
        (await VirtualFileTemplateContentContributor.GetOrNullAsync(
                new TemplateContentContributorContext(
                    await TemplateDefinitionManager.GetAsync(TestTemplates.ForgotPasswordEmail),
                    ServiceProvider,
                    null)))
            .ShouldBe(ForgotPasswordEmailEnglishContent);
    }
}
