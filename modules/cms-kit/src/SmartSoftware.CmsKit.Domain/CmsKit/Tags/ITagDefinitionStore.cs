using JetBrains.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSoftware.CmsKit.Tags;

public interface ITagDefinitionStore : IEntityTypeDefinitionStore<TagEntityTypeDefiniton>
{
    Task<List<TagEntityTypeDefiniton>> GetTagEntityTypeDefinitionListAsync();
}
