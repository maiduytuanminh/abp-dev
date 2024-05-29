using JetBrains.Annotations;
using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.CmsKit;

public interface IEntityTypeDefinitionStore<TPolicyDefinition> : ITransientDependency
    where TPolicyDefinition : EntityTypeDefinition
{
    Task<TPolicyDefinition> GetAsync([NotNull] string entityType);

    Task<bool> IsDefinedAsync([NotNull] string entityType);
}
