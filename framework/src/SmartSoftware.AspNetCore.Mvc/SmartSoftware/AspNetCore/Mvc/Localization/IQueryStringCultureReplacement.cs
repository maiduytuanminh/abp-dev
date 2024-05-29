using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Mvc.Localization;

public interface IQueryStringCultureReplacement
{
    Task ReplaceAsync(QueryStringCultureReplacementContext context);
}
