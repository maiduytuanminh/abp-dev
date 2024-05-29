using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.Localization;
using SmartSoftware.OpenIddict.Applications;

namespace MyCompanyName.MyProjectName.Pages;

public class IndexModel : SmartSoftwarePageModel
{
    public List<OpenIddictApplication>? Applications { get; protected set; }

    public IReadOnlyList<LanguageInfo>? Languages { get; protected set; }

    public string? CurrentLanguage { get; protected set; }

    protected IOpenIddictApplicationRepository OpenIdApplicationRepository { get; }

    protected ILanguageProvider LanguageProvider { get; }

    public IndexModel(IOpenIddictApplicationRepository openIdApplicationRepository, ILanguageProvider languageProvider)
    {
        OpenIdApplicationRepository = openIdApplicationRepository;
        LanguageProvider = languageProvider;
    }

    public async Task OnGetAsync()
    {
        Applications = await OpenIdApplicationRepository.GetListAsync();

        Languages = await LanguageProvider.GetLanguagesAsync();
        CurrentLanguage = CultureInfo.CurrentCulture.DisplayName;
    }
}
