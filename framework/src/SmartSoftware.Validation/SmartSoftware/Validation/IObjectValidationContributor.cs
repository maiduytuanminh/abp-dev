using System.Threading.Tasks;

namespace SmartSoftware.Validation;

public interface IObjectValidationContributor
{
    Task AddErrorsAsync(ObjectValidationContext context);
}
