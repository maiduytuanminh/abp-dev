using System.Threading.Tasks;

namespace SmartSoftware.TextTemplating;

public interface ITemplateContentContributor
{
    Task<string?> GetOrNullAsync(TemplateContentContributorContext context);
}
