using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Validation;

public class ObjectValidator : IObjectValidator, ITransientDependency
{
    protected IServiceScopeFactory ServiceScopeFactory { get; }
    protected SmartSoftwareValidationOptions Options { get; }

    public ObjectValidator(IOptions<SmartSoftwareValidationOptions> options, IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory;
        Options = options.Value;
    }

    public virtual async Task ValidateAsync(object validatingObject, string? name = null, bool allowNull = false)
    {
        var errors = await GetErrorsAsync(validatingObject, name, allowNull);

        if (errors.Any())
        {
            throw new SmartSoftwareValidationException(
                "Object state is not valid! See ValidationErrors for details.",
                errors
            );
        }
    }

    public virtual async Task<List<ValidationResult>> GetErrorsAsync(object validatingObject, string? name = null, bool allowNull = false)
    {
        if (validatingObject == null)
        {
            if (allowNull)
            {
                return new List<ValidationResult>(); //TODO: Returning an array would be more performent
            }
            else
            {
                return new List<ValidationResult>
                    {
                        name == null
                            ? new ValidationResult("Given object is null!")
                            : new ValidationResult(name + " is null!", new[] {name})
                    };
            }
        }

        var context = new ObjectValidationContext(validatingObject);

        using (var scope = ServiceScopeFactory.CreateScope())
        {
            foreach (var contributorType in Options.ObjectValidationContributors)
            {
                var contributor = (IObjectValidationContributor)
                    scope.ServiceProvider.GetRequiredService(contributorType);
                await contributor.AddErrorsAsync(context);
            }
        }

        return context.Errors;
    }
}
